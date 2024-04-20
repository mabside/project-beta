using Byhands.Application.Interfaces.Providers;
using Byhands.DataAccess;
using Byhands.Domain.DTOs.Products;
using Byhands.Domain.Entities.Products;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;
using MediatR;

namespace Byhands.Application.Usecases.Products.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<NewProduct>>
{
    private readonly IUnitOfWork uow;
    private readonly IUploadService uploadService;

    public CreateProductCommandHandler(
        IUnitOfWork uow,
        IUploadService uploadService)
    {
        this.uow = uow;
        this.uploadService = uploadService;
    }

    public async Task<Result<NewProduct>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productCategory = await uow.productCategoryRepository().GetAsync(command.ProductCategoryId);

        if (productCategory == null)
            return new NullError($"Product category with id {command.ProductCategoryId} cannot be found");

        var business = await uow.BusinessRepository().FirstOrDefaultAsync(
            business => business.Id == command.BusinessId);

        if (business == null)
            return new NullError($"business id {command.BusinessId} cannot be found for user");

        var uploadResult = await this.uploadService.UploadFile(command.Image);

        if (uploadResult.HasError)
            return uploadResult.Error;

        var imageUrl = uploadResult.Value;

        var result = Product.Create(
            name: command.Name,
            description: command.Description,
            productCategoryId: command.ProductCategoryId,
            businessId: command.BusinessId,
            imageUrl: imageUrl);

        if (result.HasError)
            return result.Error;

        var newProduct = result.Value;

        newProduct = newProduct.EmbedSlug();

        uow.ProductRepository().Add(newProduct);
        await uow.CommitAsync(cancellationToken);

        return new NewProduct(newProduct.Id);
    }
}
