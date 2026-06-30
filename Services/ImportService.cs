using ClosedXML.Excel;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enum;
using Entities.Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Services
{
    public class ImportService : IImportService
    {
        private readonly ApplicationDbContext _db;

        public ImportService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ImportResponse> ImportAsync(
            Stream stream,
            ImportType importType)
        {
            return importType switch
            {
                ImportType.EmployeeJob => await ImportEmployeeAndJobs(stream),

                ImportType.Product => await ImportProducts(stream),

                _ => throw new ArgumentException("Invalid import type.")
            };
        }
        private Dictionary<string, int> GetColumnIndexes(IXLWorksheet worksheet)
        {
            Dictionary<string, int> columns =
                new(StringComparer.OrdinalIgnoreCase);

            foreach (var cell in worksheet.Row(1).CellsUsed())
            {
                columns[cell.GetString().Trim()] =
                    cell.Address.ColumnNumber;
            }

            return columns;
        }

        private async Task<ImportResponse> ImportEmployeeAndJobs(Stream stream)
        {
            ImportResponse response = new();

            try
            {
                using var workbook = new XLWorkbook(stream);

                var sheet = workbook.Worksheet(1);

                var columns = GetColumnIndexes(sheet);

                List<AppJob> jobs = new();
                List<AppEmployee> employees = new();

                HashSet<int> existingJobs =
                    (await _db.AppJobs
                        .Select(x => x.JobID)
                        .ToListAsync())
                    .ToHashSet();

                HashSet<int> existingEmployees =
                    (await _db.AppEmployees
                        .Select(x => x.EmployeeID)
                        .ToListAsync())
                    .ToHashSet();

                foreach (var row in sheet.RowsUsed().Skip(1))
                {
                    int jobId =
                        row.Cell(columns["JobID"]).GetValue<int>();

                    if (!existingJobs.Contains(jobId))
                    {
                        jobs.Add(new AppJob
                        {
                            JobID = jobId,
                            JobName = row.Cell(columns["JobName"]).GetString().Trim(),
                            DeputyName = row.Cell(columns["DeputyName"]).GetString().Trim(),
                            ManagementName = row.Cell(columns["ManagementName"]).GetString().Trim()
                        });

                        existingJobs.Add(jobId);
                    }

                    int employeeId =
                        row.Cell(columns["EmployeeID"]).GetValue<int>();

                    if (!existingEmployees.Contains(employeeId))
                    {
                        employees.Add(new AppEmployee
                        {
                            EmployeeID = employeeId,
                            FName = row.Cell(columns["FName"]).GetString().Trim(),
                            LName = row.Cell(columns["LName"]).GetString().Trim(),
                            JobID = jobId
                        });

                        existingEmployees.Add(employeeId);
                    }
                }

                if (jobs.Any())
                    await _db.AppJobs.AddRangeAsync(jobs);

                if (employees.Any())
                    await _db.AppEmployees.AddRangeAsync(employees);

                await _db.SaveChangesAsync();

                response.Success = true;
                response.ImportedCount = jobs.Count + employees.Count;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;

                response.Errors.Add(ex.Message);

                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }

                return response;
            }
        }

        private Task<ImportResponse> ImportProducts(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}