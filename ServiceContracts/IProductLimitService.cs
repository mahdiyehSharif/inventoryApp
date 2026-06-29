using InventoryApp.ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IProductLimitService
{
    Task<ProductLimitResponse> AddProductLimit(ProductLimitAddRequest request);

    Task<List<ProductLimitResponse>> GetAllProductLimits();

    Task<ProductLimitResponse?> GetProductLimitByID(Guid productLimitID);

    Task<ProductLimitResponse?> UpdateProductLimit(ProductLimitUpdateRequest request);

    Task<bool> DeleteProductLimit(Guid productLimitID);
}
}