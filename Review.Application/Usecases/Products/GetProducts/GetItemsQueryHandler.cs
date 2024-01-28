using MediatR;
using Byhands.Application.Interfaces;
using Byhands.Domain.DTOs.Products;
using Byhands.Domain.Entities.Products;
using Byhands.Entities.Errors;
using Byhands.Entities.QueryObjects;
using Byhands.Extensions;
using Byhands.Models.Bases;

namespace Byhands.Application.Usecases.Products.GetProducts;

internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<PaginatedResult<ProductInformation>>>
{
    private readonly IProductRepository repository;

    public GetProductsQueryHandler(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PaginatedResult<ProductInformation>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var ProductList = await this.repository.GetAllAsync(product =>
            product.BusinessId == request.BusinessId,
            includes: new string[] { nameof(Product.Business), nameof(Product.ProductCategory) });

        if (ProductList == null)
            return new NullError("Shop is empty");

        var ProductInfoList = ProductList.Select(Product => (ProductInformation)Product).ToList();

        return ProductInfoList.GetPagedResult(request.Query);
    }
}
