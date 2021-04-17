using System;
using System.Collections.Generic;
using System.Text;

namespace WalletConnector.Application.Infrastructure.Services.WalletService
{
    public class AccountInfoResponseDto
    {
        public Wallet Wallet { get; set; }

        public User User { get; set; }

        public int Status { get; set; }
    }

    public class User
    {
        public string Phone { get; set; }
        public string Iin { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string IssuedBy { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string IdentificationStatus { get; set; }
    }

    public class Wallet
    {
        public string Balance { get; set; }
        public string Pan { get; set; }
        public string CardId { get; set; }
        public string IssContractId { get; set; }
    }
}
