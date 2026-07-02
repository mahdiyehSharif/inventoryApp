using System;
using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class ProductResponse
    {
        public Guid? ProductID { get; set; }
        public string? ProductName { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public List<InventoryTransactions>? Transactions { get; set; }
        public int Quantity { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(ProductResponse)) return false;

            ProductResponse product = (ProductResponse)obj;
            return ProductID == product.ProductID &&
             ProductName == product.ProductName &&
             Quantity == product.Quantity &&
             ProductCategory == product.ProductCategory ;
            //  Transactions == product.Transactions;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ProductUpdateRequest ToProductUpdateRequest()
        {
            return new ProductUpdateRequest()
            {
                ProductID = ProductID,
                ProductName = ProductName,
                Category = ProductCategory
            };
        }

    }

    public static class ProductExtensions
    {
        public static ProductResponse ToProductResponse(this AppProduct product)
        {
            return new ProductResponse()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductCategory = product.Category,
                Transactions = product.Transactions
            };
        }
    }
}