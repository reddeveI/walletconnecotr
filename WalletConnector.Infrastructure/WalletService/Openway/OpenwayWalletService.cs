using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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
using WalletConnector.Domain.Accounts;
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

        public async Task<AccountCreated> CreateAccount(string phone, string description, CancellationToken cancellationToken)
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

            var accountCreated = _mapper.Map<AccountCreated>(result);

            return accountCreated;
        }


        public async Task<PersonToPersonTransactionCreated> CreateTransaction(PersonToPersonTransaction model, CancellationToken cancellationToken)
        {
            var request = DocumentBuilder
                .CreateDefaultDocument()
                .AddTransactionType(model.MessageCode, model.TransactionType)
                .AddTransactionId(model.TransactionId)
                .AddTransactionDescription($"Перевод со счёта {model.From} на счёт {model.To}")
                .AddTransactionRequestorInfo(model.From)
                .AddTransactionDestinationInfo(model.To)
                .AddTransactionAmount(model.Currency, model.Amount);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<DocumentRequest>();

            var transactionCreated = _mapper.Map<PersonToPersonTransactionCreated>(result);

            return transactionCreated;
        }

        public async Task<WithdrawalTransactionCreated> CreateWithdrawal(WithdrawalTransaction model, CancellationToken cancellationToken)
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

            var withdrawalCreated = _mapper.Map<WithdrawalTransactionCreated>(result);

            return withdrawalCreated;
        }

        public async Task<HoldAccountCreated> HoldAccount(HoldAccount model, CancellationToken cancellationToken)
        {
            var request = DocumentBuilder
                .CreateDefaultDocument()
                .AddDocumentType(model.MessageCode)
                .AddTransactionId(model.TransactionId)
                .AddTransactionRequestorInfo(model.Phone)
                .AddTransactionSourceInfo(model.Phone)
                .AddTransactionAmount(model.Currency, model.Amount);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<DocumentRequest>();

            var accountHolded = _mapper.Map<HoldAccountCreated>(result);

            return accountHolded;
        }

        public async Task<UnholdAccountCreated> UnholdAccount(UnholdAccount model, CancellationToken cancellationToken)
        {
            var request = DocumentBuilder
                .CreateDefaultDocument()
                .AddDocumentType(model.MessageCode)
                .AddTransactionRequestorInfo(model.Phone)
                .AddTransactionSourceInfo(model.Phone)
                .AddTransactionAmount(model.Currency, model.Amount);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<DocumentRequest>();

            var accountUnholded = _mapper.Map<UnholdAccountCreated>(result);

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