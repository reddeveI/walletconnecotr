using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Serializer;
using WalletConnector.Serializer.Models.Application;
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

            return account;
        }


        public async Task<int> CreateAccount(string phone, string description, CancellationToken cancellationToken)
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

            return 1;
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