using System;
using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class EmployeeResponse
    {
        public int EmployeeID { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public AppJob? Job { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(EmployeeResponse)) return false;

            EmployeeResponse employee = (EmployeeResponse)obj;
            return EmployeeID == employee.EmployeeID &&
            FName == employee.FName && 
            LName == employee.LName &&
            Job == employee.Job;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
                Job = employee.Job
            };
        }
    }
}