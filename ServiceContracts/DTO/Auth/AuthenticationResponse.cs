namespace ServiceContracts.DTO.Auth

{
    public class AuthenticationResponse
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public string? UserName { get; set; }

        public int EmployeeID { get; set; }

        public IList<string>? Roles { get; set; }
    }
}