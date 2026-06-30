using ServiceContracts.DTO;
using ServiceContracts.DTO.Enum;

namespace ServiceContracts
{
    public interface IImportService
    {
        Task<ImportResponse> ImportAsync(Stream stream, ImportType importType);
    }
}