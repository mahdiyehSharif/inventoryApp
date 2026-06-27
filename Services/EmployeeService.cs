using Entities;
using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.ServiceContracts.DTO;
using ServiceContracts.DTO;
using InventoryApp.ServiceContracts;
using InventoryApp.Services.Helpers;
using Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        public EmployeeService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }


        private EmployeeResponse? ConvertEmployeeToEmployeeResponse(AppEmployee employee)
        {
            EmployeeResponse employeeResponse = employee.ToEmployeeResponse();
            return employeeResponse;

        }


        public async Task<EmployeeResponse> AddEmployee(EmployeeAddRequest employeeAddRequest)
        {
            if (employeeAddRequest == null)
                throw new ArgumentNullException(nameof(employeeAddRequest));

            ValidationHelper.ModelValidation(employeeAddRequest);

            AppEmployee employee = employeeAddRequest.ToEmployee();

            _db.AppEmployees.Add(employee);

            await _db.SaveChangesAsync();

            return ConvertEmployeeToEmployeeResponse(employee);
        }

        public async Task<List<EmployeeResponse>> GetAllEmployees()
        {
            return await _db.AppEmployees
                .Select(e => e.ToEmployeeResponse())
                .ToListAsync();
        }


        public async Task<EmployeeResponse?> GetEmployeeByEmployeeID(int employeeID)
        {
            AppEmployee? employee =
                await _db.AppEmployees
                .FirstOrDefaultAsync(e => e.EmployeeID == employeeID);

            if (employee == null)
                return null;

            return employee.ToEmployeeResponse();
        }

        public async Task<List<EmployeeResponse>> GetFilteredEmployees(
    string searchBy,
    string? searchString)
        {
            List<EmployeeResponse> allEmployees = await GetAllEmployees();

            if (string.IsNullOrWhiteSpace(searchBy) ||
                string.IsNullOrWhiteSpace(searchString))
            {
                return allEmployees;
            }

            searchString = searchString.Trim();

            return searchBy switch
            {
                nameof(EmployeeResponse.FName) =>
                    allEmployees
                    .Where(e => !string.IsNullOrEmpty(e.FName) &&
                                e.FName.Contains(searchString,
                                StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(EmployeeResponse.LName) =>
                    allEmployees
                    .Where(e => !string.IsNullOrEmpty(e.LName) &&
                                e.LName.Contains(searchString,
                                StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                _ => allEmployees
            };
        }

        public async Task<List<EmployeeResponse>> GetSortedEmployees(
     List<EmployeeResponse> allEmployees,
     string sortBy,
     SortOrderOptions sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return allEmployees;

            List<EmployeeResponse> sortedEmployees =
                (sortBy, sortOrder) switch
                {
                    (nameof(EmployeeResponse.FName), SortOrderOptions.ASC) =>
                        allEmployees
                        .OrderBy(e => e.FName)
                        .ToList(),

                    (nameof(EmployeeResponse.FName), SortOrderOptions.DESC) =>
                        allEmployees
                        .OrderByDescending(e => e.FName)
                        .ToList(),

                    (nameof(EmployeeResponse.LName), SortOrderOptions.ASC) =>
                        allEmployees
                        .OrderBy(e => e.LName)
                        .ToList(),

                    (nameof(EmployeeResponse.LName), SortOrderOptions.DESC) =>
                        allEmployees
                        .OrderByDescending(e => e.LName)
                        .ToList(),

                    _ => allEmployees
                };

            return await Task.FromResult(sortedEmployees);
        }


        public async Task<bool> DeleteEmployee(int employeeID)
        {
            var employee = await _db.AppEmployees
                .FirstOrDefaultAsync(e => e.EmployeeID == employeeID);

            if (employee == null)
                return false;

            _db.AppEmployees.Remove(employee);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<EmployeeResponse?> UpdateEmployee(EmployeeUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            AppEmployee? employee = await _db.AppEmployees
                .FirstOrDefaultAsync(e => e.EmployeeID == request.EmployeeID);

            if (employee == null)
                return null;

            employee.FName = request.FName;
            employee.LName = request.LName;
            employee.JobID = request.JobID;

            await _db.SaveChangesAsync();

            return employee.ToEmployeeResponse();
        }
    }
}

