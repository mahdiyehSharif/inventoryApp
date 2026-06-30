using ServiceContracts.DTO.Enum;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts.DTO
{
    public class ImportAddRequest
    {
        public ImportType ImportType { get; set; }
    }
}