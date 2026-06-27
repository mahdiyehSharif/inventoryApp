using InventoryApp.ServiceContracts.DTO;
using ServiceContracts.DTO;
using InventoryApp.ServiceContracts.DTO.Enums;

namespace InventoryApp.ServiceContracts
{
    public interface IEmployeeService
    {
        Task<EmployeeResponse> AddEmployee(EmployeeAddRequest employeeAddRequest);

        Task<List<EmployeeResponse>> GetAllEmployees();

        Task<EmployeeResponse?> GetEmployeeByEmployeeID(int employeeID);

        Task<List<EmployeeResponse>> GetFilteredEmployees(
            string searchBy,
            string? searchString);

        Task<List<EmployeeResponse>> GetSortedEmployees(
            List<EmployeeResponse> allEmployees,
            string sortBy,
            SortOrderOptions sortOrder);

        Task<EmployeeResponse?> UpdateEmployee(
            EmployeeUpdateRequest employeeUpdateRequest);

        Task<bool> DeleteEmployee(int employeeID);
    }
}