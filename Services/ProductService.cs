using Entities;
using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.ServiceContracts.DTO;
using InventoryApp.ServiceContracts;
using InventoryApp.Services.Helpers;
using Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }


        private ProductResponse ConvertProductToProductResponse(AppProduct product)
        {
            ProductResponse productResponse = product.ToProductResponse();
            return productResponse;

        }


        public async Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest)
        {

            if (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }
            ValidationHelper.ModelValidation(productAddRequest);


            if (string.IsNullOrEmpty(productAddRequest.ProductName?.ToString()))
            {
                throw new ArgumentException("ProductName can not be blank");
            }

            AppProduct product = productAddRequest.ToProduct();
            product.ProductID = Guid.NewGuid();

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return ConvertProductToProductResponse(product);
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            return await _db.Products
               .Select(p => p.ToProductResponse())
               .ToListAsync();
        }


        public async Task<ProductResponse?> GetProductByProductID(Guid productID)
        {
            AppProduct? product = await _db.Products
            .FirstOrDefaultAsync(p => p.ProductID == productID);

            if (product == null)
                return null;

            return product.ToProductResponse();
        }

        public async Task<List<ProductResponse>> GetFilteredProducts(string searchBy, string searchString)
        {
            List<ProductResponse> allProducts = await GetAllProducts();

            List<ProductResponse> matchingProducts = allProducts;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingProducts;

            switch (searchBy)
            {
                case nameof(AppProduct.ProductName):
                    matchingProducts = allProducts.Where(temp =>
                    string.IsNullOrEmpty(temp.ProductName) || temp.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(AppProduct.Category):
                    matchingProducts = allProducts.Where(temp =>
                        temp.ProductCategory.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                    break;

                default: matchingProducts = allProducts; break;
            }
            return matchingProducts;
        }

        public Task<List<ProductResponse>> GetSortedProducts(List<ProductResponse> allProducts, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return Task.FromResult(allProducts);

            List<ProductResponse> sortedProducts = (sortBy, sortOrder)
            switch
            {
                (nameof(ProductResponse.ProductName), SortOrderOptions.ASC)
                => allProducts.OrderBy(temp => temp.ProductName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ProductResponse.ProductName), SortOrderOptions.DESC)
                => allProducts.OrderByDescending(temp => temp.ProductName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ProductResponse.ProductCategory), SortOrderOptions.ASC)
                => allProducts.OrderBy(temp => temp.ProductCategory.ToString()).ToList(),

                (nameof(ProductResponse.ProductCategory), SortOrderOptions.DESC)
                => allProducts.OrderByDescending(temp => temp.ProductCategory.ToString()).ToList(),

                _ => allProducts
            };
            return Task.FromResult(sortedProducts);
        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            if (productUpdateRequest == null)
                throw new ArgumentNullException(nameof(ProductUpdateRequest));
            ValidationHelper.ModelValidation(productUpdateRequest);

            AppProduct? matchingProduct = await _db.Products.FirstOrDefaultAsync(
                         p => p.ProductID == productUpdateRequest.ProductID);
            if (matchingProduct == null)
            {
                throw new ArgumentException("Given ProductID does not exist.");
            }

            matchingProduct.ProductName = productUpdateRequest.ProductName;
            matchingProduct.Category = productUpdateRequest.Category;
            await _db.SaveChangesAsync();

            return matchingProduct.ToProductResponse();
        }

        public async Task<bool> DeleteProduct(Guid productID)
        {
            AppProduct? product = await _db.Products
                    .FirstOrDefaultAsync(p => p.ProductID == productID);
            if (product == null)
                return false;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}

