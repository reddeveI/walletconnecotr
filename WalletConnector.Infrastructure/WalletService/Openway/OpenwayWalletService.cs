using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Accounts.Commands.HoldAccount;
using WalletConnector.Application.Accounts.Commands.UnholdAccount;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;
using WalletConnector.Application.Transactions.Commands.CreateWithdrawalTransaction;
using WalletConnector.Domain.Transactrions;
using WalletConnector.Serializer;
using WalletConnector.Serializer.Models.Application;
using WalletConnector.Serializer.Models.Document;
using WalletConnector.Serializer.Models.Information;

namespace WalletConnector.Infrastructure.WalletService.Openway
{
    public class OpenwayWalletService : IWalletService
    {
        private readonly ILogger<OpenwayWalletService> _logger;
        private readonly WalletServiceConfig _config;
        private readonly IMapper _mapper;

        public OpenwayWalletService(
            ILogger<OpenwayWalletService> logger,
            IOptions<WalletServiceConfig> config,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config.Value;
            _mapper = mapper;
        }

        public async Task<AccountInfoResponseDto> GetAccountInfo(string phone)
        {
            var request = InformationBuilder
                .CreateDefaultInformation()
                .AddResultDetails()
                .AddPhoneNumber(phone);

            var xmlMessage = request.ToXElement().ToString();
            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);
            var result = response.FromXElement<InformationRequest>();

            AccountInfoResponseDto account = _mapper.Map<AccountInfoResponseDto>(result);

            if (account.Status != 0) 
            {
                throw new NotFoundException();
            }

            return account;
        }

        public async Task<AccountCreatedVm> CreateAccount(string phone, string description, CancellationToken cancellationToken)
        {
            var request = ApplicationBuilder
                .CreateDefaultApplication()
                .AddResultDetails()
                .AddPhoneNumber(phone)
                .AddClientData(phone)
                .AddSubApplication(phone);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<ApplicationRequest>();

            AccountCreatedVm accountCreated = _mapper.Map<AccountCreatedVm>(result);

            return accountCreated;
        }


        public async Task<TransactionCreatedVm> CreateTransaction(string from, string to, decimal amount, string currency, string messageCode, string transactionType, CancellationToken cancellationToken, string transactionId = null)
        {
            var request = DocumentBuilder
                .CreateDefaultDocument()
                .AddTransactionType(messageCode, transactionType)
                .AddTransactionId(transactionId)
                .AddTransactionDescription($"Перевод со счёта {from} на счёт {to}")
                .AddTransactionRequestorInfo(from)
                .AddTransactionDestinationInfo(to)
                .AddTransactionAmount(currency, amount);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<DocumentRequest>();

            TransactionCreatedVm transactionCreated = _mapper.Map<TransactionCreatedVm>(result);

            return transactionCreated;
        }

        public async Task<WithdrawalCreatedVm> CreateWithdrawal(WithdrawalTransaction model, CancellationToken cancellationToken)
        {
            var request = DocumentBuilder
                .CreateDefaultDocument()
                .AddTransactionCode(model.MessageCode, model.TransactionType)
                .AddTransactionId(model.TransactionId)
                .AddTransactionDescription(model.Description)
                .AddTransactionRequestorInfo(model.Phone)
                .AddTransactionSourceInfo(model.Phone)
                .AddTransactionAmount(model.Currency, model.Amount)
                .AddExtraAmount(model.Commission);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<DocumentRequest>();

            WithdrawalCreatedVm withdrawalCreated = _mapper.Map<WithdrawalCreatedVm>(result);

            return withdrawalCreated;
        }


        public async Task<AccountHoldedVm> HoldAccount(string phone, decimal amount, string currency, string messageCode, string transactionId, CancellationToken cancellationToken)
        {
            var request = DocumentBuilder
                .CreateDefaultDocument()
                .AddDocumentType(messageCode)
                .AddTransactionId(transactionId)
                .AddTransactionRequestorInfo(phone)
                .AddTransactionSourceInfo(phone)
                .AddTransactionAmount(currency, amount);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<DocumentRequest>();

            AccountHoldedVm accountHolded = _mapper.Map<AccountHoldedVm>(result);

            return accountHolded;
        }

        public async Task<AccountUnholdedVm> UnholdAccount(string phone, decimal amount, string currency, string messageCode, CancellationToken cancellationToken)
        {
            var request = DocumentBuilder
                .CreateDefaultDocument()
                .AddDocumentType(messageCode)
                .AddTransactionRequestorInfo(phone)
                .AddTransactionSourceInfo(phone)
                .AddTransactionAmount(currency, amount);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<DocumentRequest>();

            AccountUnholdedVm accountUnholded = _mapper.Map<AccountUnholdedVm>(result);

            return accountUnholded;
        }

        private async Task<string> _sendWalletRequest(string url, string xmlMessage)
        {
            using (var wc = new WebClient())
            {
                _logger.LogInformation("request: {xmlMessage}", xmlMessage);
                var response = await wc.UploadStringTaskAsync(url, xmlMessage);
                _logger.LogInformation("response: {response}", response);
                return response;
            }
        }
    }
}