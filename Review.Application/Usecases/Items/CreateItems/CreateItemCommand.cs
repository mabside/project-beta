using MediatR;
using Review.Domain.DTOs.Items;
using Review.Models.Bases;

namespace Review.Application.Usecases.Items.CreateProduct;

public record CreateItemCommand(
    Guid UniqueRequestId,
    string Name,
    string Description,
    string ImageUrl) : IRequest<Result<NewItem>>;
