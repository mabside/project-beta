using MediatR;
using Microsoft.AspNetCore.Http;
using Byhands.Domain.DTOs.Products;
using Byhands.Models.Bases;

namespace Byhands.Application.Usecases.Products.CreateProduct;

public record CreateProductCommand(
    Guid UniqueRequestId,
    Guid ProductCategoryId,
    Guid BusinessId,
    string Name,
    string Description,
    IFormFile Image) : IRequest<Result<NewProduct>>;
