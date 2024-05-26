using Byhands.DataAccess;
using Byhands.Domain.Entities.Products;

namespace Byhands.Contract.Interfaces;

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