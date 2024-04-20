using Byhands.DataAccess;
using Byhands.Domain.Entities.Products;

namespace Byhands.Application.Interfaces;

public interface IProductRepository
    : IWriteOnlyRepository<Product, Guid>,
    IReadOnlyRepository<Product, Guid>
{
}

public interface IProductCategoryRepository
    : IWriteOnlyRepository<ProductCategory, Guid>,
    IReadOnlyRepository<ProductCategory, Guid>
{
}