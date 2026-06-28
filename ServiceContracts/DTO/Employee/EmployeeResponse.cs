using Entities;
using ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts.DTO
{
    public class EmployeeResponse
    {
        public int EmployeeID { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public int JobID { get; set; }

        public string? JobName { get; set; }

        public string FullName => $"{FName} {LName}";

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(EmployeeResponse)) return false;

            EmployeeResponse employee = (EmployeeResponse)obj;

            return EmployeeID == employee.EmployeeID &&
                   FName == employee.FName &&
                   LName == employee.LName &&
                   JobName == employee.JobName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public EmployeeUpdateRequest ToEmployeeUpdateRequest()
        {
            return new EmployeeUpdateRequest()
            {
                EmployeeID = EmployeeID,
                FName = FName,
                LName = LName,
                JobID = JobID
            };
        }
    }

    public static class EmployeeExtensions
    {
        public static EmployeeResponse ToEmployeeResponse(this AppEmployee? employee)
        {
            return new EmployeeResponse()
            {
                EmployeeID = employee.EmployeeID,
                FName = employee.FName,
                LName = employee.LName,
                JobID = employee.JobID,
                JobName = employee.JobName
            };
        }
    }
}