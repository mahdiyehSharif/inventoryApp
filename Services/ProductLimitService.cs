using Entities;
using Entities.Data;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using ServiceContracts;
using InventoryApp.Services.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProductLimitService : IProductLimitService
    {
        private readonly ApplicationDbContext _db;

        public ProductLimitService(ApplicationDbContext db)
        {
            _db = db;
        }

        private ProductLimitResponse ConvertToResponse(ProductLimit productLimit)
        {
            return productLimit.ToProductLimitResponse();
        }

        public async Task<ProductLimitResponse> AddProductLimit(ProductLimitAddRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            ProductLimit productLimit = request.ToProductLimit();

            productLimit.ProductLimitID = Guid.NewGuid();

            _db.ProductLimits.Add(productLimit);

            await _db.SaveChangesAsync();

            productLimit = await _db.ProductLimits
                .Include(p => p.Product)
                .Include(p => p.Job)
                .FirstAsync(p => p.ProductLimitID == productLimit.ProductLimitID);

            return ConvertToResponse(productLimit);
        }

        public async Task<List<ProductLimitResponse>> GetAllProductLimits()
        {
            return await _db.ProductLimits
                .Include(p => p.Product)
                .Include(p => p.Job)
                .Select(p => p.ToProductLimitResponse())
                .ToListAsync();
        }

        public async Task<ProductLimitResponse?> GetProductLimitByID(Guid productLimitID)
        {
            ProductLimit? productLimit = await _db.ProductLimits
                .Include(p => p.Product)
                .Include(p => p.Job)
                .FirstOrDefaultAsync(p => p.ProductLimitID == productLimitID);

            if (productLimit == null)
                return null;

            return ConvertToResponse(productLimit);
        }

        public async Task<ProductLimitResponse?> UpdateProductLimit(ProductLimitUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            ProductLimit? productLimit = await _db.ProductLimits
                .FirstOrDefaultAsync(p => p.ProductLimitID == request.ProductLimitID);

            if (productLimit == null)
                return null;

            productLimit.ProductID = request.ProductID;
            productLimit.JobID = request.JobID;
            productLimit.MaxQuantity = request.MaxQuantity;
            productLimit.PeriodValue = request.PeriodValue;
            productLimit.PeriodType = request.PeriodType;
            productLimit.IsActive = request.IsActive;

            await _db.SaveChangesAsync();

            productLimit = await _db.ProductLimits
                .Include(p => p.Product)
                .Include(p => p.Job)
                .FirstAsync(p => p.ProductLimitID == request.ProductLimitID);

            return ConvertToResponse(productLimit);
        }

        public async Task<bool> DeleteProductLimit(Guid productLimitID)
        {
            ProductLimit? productLimit = await _db.ProductLimits
                .FirstOrDefaultAsync(p => p.ProductLimitID == productLimitID);

            if (productLimit == null)
                return false;

            _db.ProductLimits.Remove(productLimit);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}