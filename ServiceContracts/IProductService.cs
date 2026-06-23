using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts
{
    public interface IProductService
    {
        ProductResponse AddProduct(ProductAddRequest? productAddRequest);

        List<ProductResponse> GetAllProducts();

        ProductResponse GetProductByProductID(Guid? ProductID);

        List<ProductResponse> GetFilteredProducts(string searchBy, string searchString);

        List<ProductResponse> GetSortedProducts(List<ProductResponse> allProducts, string sortBy, SortOrderOptions sortOrder);

        ProductResponse UpdateProduct (ProductUpdateRequest productUpdateRequest);

        bool DeleteProduct (Guid? productID);
    }
}
