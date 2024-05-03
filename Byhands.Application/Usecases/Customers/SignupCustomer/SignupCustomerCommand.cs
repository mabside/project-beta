using Byhands.CQRS;
using Byhands.Domain.DTOs.Customers;
using Byhands.Models.Bases;
using MediatR;

namespace Byhands.Application.Usecases.Customers.SignupCustomer;

public record SignupCustomerCommand(
    Guid CommandId,
    string firstname,
    string lastname,
    string email,
    string phoneNumber,
    string password
) : CommandBase<NewCustomer>(CommandId);
