using Entities;
using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.ServiceContracts.DTO;
using InventoryApp.ServiceContracts;
using InventoryApp.Services.Helpers;
using Entities.Data;

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


        public EmployeeResponse? AddEmployee(EmployeeAddRequest? employeeAddRequest)
        {

            if (employeeAddRequest == null)
            {
                throw new ArgumentNullException(nameof(employeeAddRequest));
            }
            ValidationHelper.ModelValidation(employeeAddRequest);


            if (string.IsNullOrEmpty(employeeAddRequest.FName?.ToString()))
            {
                throw new ArgumentException("ProductName can not be blank");
            }

            AppEmployee employee = employeeAddRequest.ToEmployee();

            _db.Add(employee);
            // _db.SaveChanges();

            return ConvertEmployeeToEmployeeResponse(employee);
        }

        public List<EmployeeResponse> GetAllEmployees()
        {
            return _db.AppEmployees.ToList()
            .Select(p => p.ToEmployeeResponse()).ToList();
        }


        public EmployeeResponse? GetEmployeeByEmployeeID(int employeeID)
        {
            if (employeeID == null)
                return null;

            AppEmployee employee = _db.AppEmployees.FirstOrDefault(e => e.EmployeeID == employeeID);
            if (employee == null)
                return null;

            // _db.SaveChanges();
            return employee.ToEmployeeResponse();
        }

        public List<EmployeeResponse> GetFilteredEmployees(string searchBy, string searchString)
        {
            List<EmployeeResponse> allEmployees = GetAllEmployees();
            List<EmployeeResponse> matchingEmployees = allEmployees ;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingEmployees;

            switch (searchBy)
            {
                case nameof(AppEmployee.FName):
                    matchingEmployees = allEmployees.Where(temp =>
                    string.IsNullOrEmpty(temp.FName) || temp.FName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(AppEmployee.LName):
                    matchingEmployees = allEmployees.Where(temp =>
                        temp.LName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                    break;

                default: matchingEmployees = allEmployees; break;
            }
            // _db.SaveChanges();
            return matchingEmployees;
        }

        public List<EmployeeResponse> GetSortedEmployees(List<EmployeeResponse> allEmployees, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allEmployees;

            List<EmployeeResponse> sortedEmployees = (sortBy, sortOrder)
            switch
            {
                (nameof(EmployeeResponse.FName), SortOrderOptions.ASC)
                => allEmployees.OrderBy(temp => temp.FName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(EmployeeResponse.LName), SortOrderOptions.DESC)
                => allEmployees.OrderByDescending(temp => temp.LName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(EmployeeResponse.Job), SortOrderOptions.ASC)
                => allEmployees.OrderBy(temp => temp.Job.ToString()).ToList(),

                _ => allEmployees
            };
            // _db.SaveChanges();
            return sortedEmployees;
        }


        public bool DeleteEmployee(int employeeID)
        {
            if (employeeID == null)
            {
                throw new ArgumentNullException(nameof(employeeID));
            }

            AppEmployee? employee = _db.AppEmployees.FirstOrDefault(temp => temp.EmployeeID == employeeID);
            if (employee == null)
                return false;

            _db.AppEmployees.Remove(_db.AppEmployees.First(temp => temp.EmployeeID == employeeID));
            _db.SaveChanges();

            return true;
        }
    }
}

