using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Services.Controllers
{
    [Route("accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}