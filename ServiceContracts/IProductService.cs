using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts
{
    public interface IProductService
    {
        Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest);

        Task<List<ProductResponse>> GetAllProducts();

        Task<ProductResponse?> GetProductByProductID(Guid productID);

        Task<List<ProductResponse>> GetFilteredProducts(
            string searchBy,
            string? searchString);

        Task<List<ProductResponse>> GetSortedProducts(
            List<ProductResponse> allProducts,
            string sortBy,
            SortOrderOptions sortOrder);

        Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);

        Task<bool> DeleteProduct(Guid productID);
    }
}
