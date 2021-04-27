using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WalletConnector.Application.Transactions.Commands.CreateExternalTransaction;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;
using WalletConnector.Application.Transactions.Commands.CreateWithdrawalTransaction;

namespace WalletConnector.Api.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public TransactionController(IMediator mediator, ILogger<TransactionController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost, Route("common/transfer")]
        public async Task<TransactionCreatedVm> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost, Route("external/transfer")]
        public async Task<TransactionCreatedVm> CreateExternalTransaction([FromBody] CreateExternalTransactionCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost, Route("withdrawal/confirm")]
        public async Task<WithdrawalCreatedVm> CreateWithdrawalTransaction([FromBody] CreateWithdrawalTransactionCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
