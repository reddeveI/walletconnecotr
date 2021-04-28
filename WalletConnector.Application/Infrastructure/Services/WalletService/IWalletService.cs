using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Accounts.Commands.HoldAccount;
using WalletConnector.Application.Accounts.Commands.UnholdAccount;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;
using WalletConnector.Application.Transactions.Commands.CreateWithdrawalTransaction;
using WalletConnector.Domain.Accounts;
using WalletConnector.Domain.Transactrions;

namespace WalletConnector.Application.Infrastructure.Services.WalletService
{
    public interface IWalletService
    {
        Task<AccountInfoResponseDto> GetAccountInfo(string phone);

        Task<AccountCreated> CreateAccount(string phone, string description, CancellationToken cancellationToken);

        Task<PersonToPersonTransactionCreated> CreateTransaction(PersonToPersonTransaction model, CancellationToken cancellationToken);

        Task<WithdrawalTransactionCreated> CreateWithdrawal(WithdrawalTransaction model, CancellationToken cancellationToken);

        Task<PaymentTransactionCreated> CreatePaymentTransaction(PaymentTransaction model, CancellationToken cancellationToken);

        Task<HoldAccountCreated> HoldAccount(HoldAccount model, CancellationToken cancellationToken);

        Task<UnholdAccountCreated> UnholdAccount(UnholdAccount model, CancellationToken cancellationToken);
    }
}
