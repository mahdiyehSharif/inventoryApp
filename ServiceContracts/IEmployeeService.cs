using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts
{
    public interface IEmployeeService
    {
        EmployeeResponse AddEmployee(EmployeeAddRequest? employeeAddRequest);

        List<EmployeeResponse> GetAllEmployees();

        EmployeeResponse GetEmployeeByEmployeeID(int EmployeeID);

        List<EmployeeResponse> GetFilteredEmployees(string searchBy, string searchString);

        List<EmployeeResponse> GetSortedEmployees(List<EmployeeResponse> allEmployees, string sortBy, SortOrderOptions sortOrder);

        // EmployeeResponse UpdateProduct (ProductUpdateRequest productUpdateRequest);

        bool DeleteEmployee (int EmployeeID);
    }
}
