using MediatR;
using Microsoft.AspNetCore.Http;
using Review.Domain.DTOs.Items;
using Review.Models.Bases;

namespace Review.Application.Usecases.Items.CreateProduct;

public record CreateItemCommand(
    Guid UniqueRequestId,
    Guid ItemCategoryId,
    Guid BusinessId,
    Guid SpaceId,
    string Name,
    string Description,
    IFormFile Image) : IRequest<Result<NewItem>>;
