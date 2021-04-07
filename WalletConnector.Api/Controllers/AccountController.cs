using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Accounts.Queries.GetAccountInfo;

namespace WalletConnector.Api.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost, Route("common/user/find")]
        public async Task<AccountInfoVm> GetAccountInfo([FromBody] GetAccountInfoQuery query)
        {
            return await _mediator.Send(query);
        }


        [HttpPost, Route("wallet/user/new")]
        public async Task<int> CreateAccount([FromBody] CreateAccountCommand command)
        {
            return await _mediator.Send(command);
        }


    }
}
