using Byhands.Contract.Interfaces;
using Byhands.DataAccess;
using Byhands.Domain.Entities.Products;

namespace Byhands.Infrastructure.DataAccess.Repositories;

internal sealed class ProductRepository
    : EFContextRepositoryBase<ByhandsDbContext, Product, Guid>, IProductRepository
{
    public ProductRepository(ByhandsDbContext context) : base(context)
    {
    }
}

internal sealed class productCategoryRepository
    : EFContextRepositoryBase<ByhandsDbContext, ProductCategory, Guid>, IProductCategoryRepository
{
    public productCategoryRepository(ByhandsDbContext context) : base(context)
    {
    }
}
