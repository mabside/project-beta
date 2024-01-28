using MediatR;
using Byhands.DataAccess;
using Byhands.Domain.DTOs.Businesses;
using Byhands.Domain.Entities.Businesses;
using Byhands.Models.Bases;

namespace Byhands.Application.Usecases.Businesses.CeateBusinesses;

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, Result<NewBusiness>>
{
    private readonly IUnitOfWork uow;

    public CreateBusinessCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<Result<NewBusiness>> Handle(
        CreateBusinessCommand command,
        CancellationToken cancellationToken)
    {
        var newLocationResult = Location.Create(
            number: command.Number,
            city: command.City,
            state: command.State,
            street: command.Street,
            country: command.Country,
            postalCode: command.PostalCode);

        if (newLocationResult.HasError)
            return newLocationResult.Error;

        var newLocation = newLocationResult.Value;

        var businessCategory = await uow.BusinessCategoryRepository()
            .GetAsync(command.BusinessCategoryId);

        if (businessCategory == null)
            return new Error(
                "invalid business category",
                "Invalid.Category",
                false);

        var customer = await uow.CustomerRepository()
            .GetAsync(command.CustomerId);

        if (customer == null)
            return new Error(
                "invalid customer",
                "Invalid.Customer",
                false);

        var newBusinessResult = Business.Create(
            name: command.Name,
            description: command.Description,
            email: command.Email,
            logoUrl: command.LogoUrl,
            bannerUrl: command.BannerUrl,
            websiteUrl: command.WebsiteUrl,
            businessCategoryId: command.BusinessCategoryId,
            category: businessCategory,
            customerId: command.CustomerId,
            customer: customer,
            location: newLocation,
            socialHandles: null);

        if (newBusinessResult.HasError)
            return newBusinessResult.Error;

        var newBusiness = newBusinessResult.Value;

        newBusiness = newBusiness.EmbedSlug();

        uow.BusinessRepository().Add(newBusiness);
        await uow.CommitAsync(cancellationToken);

        return new NewBusiness(newBusiness.Id);
    }
}
