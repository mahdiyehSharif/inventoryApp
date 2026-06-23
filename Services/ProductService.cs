using Entities;
using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.ServiceContracts.DTO;
using InventoryApp.ServiceContracts;
using InventoryApp.Services.Helpers;
using Entities.Data;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }


        private ProductResponse? ConvertProductToProductResponse(AppProduct product)
        {
            ProductResponse productResponse = product.ToProductResponse();
            return productResponse;

        }


        public ProductResponse? AddProduct(ProductAddRequest? productAddRequest)
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

            _db.Add(product);
            // _db.SaveChanges();

            return ConvertProductToProductResponse(product);
        }

        public List<ProductResponse> GetAllProducts()
        {
            return _db.Products.ToList()
            .Select(p => p.ToProductResponse()).ToList();
        }


        public ProductResponse? GetProductByProductID(Guid? productID)
        {
            if (productID == null)
                return null;

            AppProduct product = _db.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product == null)
                return null;

            // _db.SaveChanges();
            return product.ToProductResponse();
        }

        public List<ProductResponse> GetFilteredProducts(string searchBy, string searchString)
        {
            List<ProductResponse> allProducts = GetAllProducts();
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
            // _db.SaveChanges();
            return matchingProducts;
        }

        public List<ProductResponse> GetSortedProducts(List<ProductResponse> allProducts, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allProducts;

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
            // _db.SaveChanges();
            return sortedProducts;
        }

        public ProductResponse UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            if (productUpdateRequest == null)
                throw new ArgumentNullException(nameof(ProductUpdateRequest));
            ValidationHelper.ModelValidation(productUpdateRequest);

            AppProduct? matchingProduct = _db.Products.FirstOrDefault(temp => temp.ProductID == productUpdateRequest.ProductID);
            if (matchingProduct == null)
            {
                throw new ArgumentException("Given ProductID does not exist.");
            }

            matchingProduct.ProductName = productUpdateRequest.ProductName;
            matchingProduct.Category = productUpdateRequest.Category;
            // matchingProduct.Transactions = productUpdateRequest.Transactions.ToList();
            _db.SaveChanges();

            return matchingProduct.ToProductResponse();
        }

        public bool DeleteProduct(Guid? productID)
        {
            if (productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            AppProduct? product = _db.Products.FirstOrDefault(temp => temp.ProductID == productID);
            if (product == null)
                return false;

            _db.Products.Remove(_db.Products.First(temp => temp.ProductID == productID));
            _db.SaveChanges();

            return true;
        }
    }
}

