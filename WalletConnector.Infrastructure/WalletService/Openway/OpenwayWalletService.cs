﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Infrastructure.WalletService.Openway.Models;
using WalletConnector.Infrastructure.WalletService.Openway.Models.Information;

namespace WalletConnector.Infrastructure.WalletService.Openway
{
    public class OpenwayWalletService : IWalletService
    {
        private readonly ILogger<OpenwayWalletService> _logger;
        private readonly WalletServiceConfig _config;

        public OpenwayWalletService(
            ILogger<OpenwayWalletService> logger,
            IOptions<WalletServiceConfig> config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config.Value;
        }

        

        public async Task<AccountInfoResponseDto> GetAccountInfo(string phone)
        {
            var request = InformationBuilder
                .CreateInformationRequest()
                .GenerateMessageId()
                .AddSourceAttribute()
                .GenerateRegNumber()
                .AddObjectType()
                .AddActionType()
                .AddResultDetails()
                .AddFilters()
                .AddPhoneNumber(phone);

            var xmlMessage = request.ToXElement().ToString();

            var response = await _sendWalletRequest(url: _config.Url, xmlMessage: xmlMessage);

            var result = response.FromXElement<InformationRequest>();

            var info = new AccountInfoResponseDto
            {
                Actual = new AccountInfoResponseDto.ActualWallet
                {
                    Wallets = new List<AccountInfoResponseDto.Wallet>()
                }
            };

            info.Actual.Wallets.Add(new AccountInfoResponseDto.Wallet { Pan = result.MsgData.Information.DataRs.ContractRs[0].RsContract.ContractIdt.ContractNumber });

            return info;
        }


        public async Task<int> CreateAccount(string phone, string description)
        {
            return 1;
        }

        private async Task<string> _sendWalletRequest(string url, string xmlMessage)
        {
            using (var wc = new WebClient())
            {
                _logger.LogDebug("request: {xmlMessage}", xmlMessage);
                var response = await wc.UploadStringTaskAsync(url, xmlMessage);
                _logger.LogInformation("response: {response}", response);
                return response;
            }
        }
    }
}
