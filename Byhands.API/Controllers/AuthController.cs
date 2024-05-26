using Byhands.API.Models.Auth.Requests;
using Byhands.API.Models.Auth.Responses;
using Byhands.Application.Usecases.Customers.SigninCustomer;
using Byhands.Application.Usecases.Customers.SignupCustomer;
using Byhands.Domain.DTOs.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Byhands.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISender sender;

        public AuthController(ISender sender)
        {
            this.sender = sender;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> CustomerSignUp([FromBody] SignupRequest request)
        {
            var cancTokenSource = new CancellationTokenSource();

            var command = (SignupCustomerCommand)request;

            var result = await sender.Send(command, cancTokenSource.Token);

            if (result.HasError)
                return BadRequest(result.Error);

            var response = new Response<NewCustomerResponse>
            {
                Data = result.Value,
                IsSuccessful = true,
                Message = "customer created successfully",
                Code = "00"
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> CustomerSignIn([FromBody] SigninRequest request)
        {
            var cancTokenSource = new CancellationTokenSource();

            var command = (SigninCustomerCommand)request;

            var result = await sender.Send(command, cancTokenSource.Token);

            if (result.HasError)
                return BadRequest(result.Error);

            var response = new Response<SigninCustomerResponse>
            {
                Data = result.Value,
                IsSuccessful = true,
                Message = "login was successfully",
                Code = "00"
            };

            return Ok(response);
        }
    }
}
