﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Accounts.Commands.HoldAccount;
using WalletConnector.Application.Accounts.Commands.UnholdAccount;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;
using WalletConnector.Application.Transactions.Commands.CreateWithdrawalTransaction;

namespace WalletConnector.Application.Infrastructure.Services.WalletService
{
    public interface IWalletService
    {
        Task<AccountInfoResponseDto> GetAccountInfo(string phone);

        Task<AccountCreatedVm> CreateAccount(string phone, string description, CancellationToken cancellationToken);

        Task<TransactionCreatedVm> CreateTransaction(string from, string to, decimal amount, string currency, string messageCode, string transactionType, CancellationToken cancellationToken, string transactionId);

        Task<WithdrawalCreatedVm> CreateWithdrawal(string phone, string transactionId, string description, decimal amount, decimal commission, string currency, string messageCode, string transactionType, CancellationToken cancellationToken);

        Task<AccountHoldedVm> HoldAccount(string phone, decimal amount, string currency, string messageCode, string transactionId, CancellationToken cancellationToken);

        Task<AccountUnholdedVm> UnholdAccount(string phone, decimal amount, string currency, string messageCode, CancellationToken cancellationToken);
    }
}
