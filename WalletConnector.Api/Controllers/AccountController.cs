using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Accounts.Commands.HoldAccount;
using WalletConnector.Application.Accounts.Commands.UnholdAccount;
using WalletConnector.Application.Accounts.Queries.CheckForPayment;
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
        [ProducesResponseType(typeof(AccountCreatedVm), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Created("common/user/find", result);
        }

        [HttpPost, Route("paynet/service/check")]
        public async Task<PaymentCheckVm> CheckForPaymentAvalibility([FromBody] CheckForPaymentQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost, Route("withdrawal/hold")]
        public async Task<AccountHoldedVm> HoldAccount([FromBody] HoldAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost, Route("withdrawal/unhold")]
        public async Task<AccountUnholdedVm> UnholdAccount([FromBody] UnholdAccountCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
