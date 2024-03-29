﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Queries.GetAccountInfo
{
    public class AccountInfoVm
    {
        public List<ActualWallet> Actual { get; set; }

        public class ActualWallet
        {
            public UserWallet Wallet { get; set; }
        }

        public class UserWallet
        {
            public string Balance { get; set; }
            public string Pan { get; set; }
            public string CardId { get; set; }
            public string IssContractId { get; set; }
            public UserData UserData { get; set; } 
        }

        public class UserData
        {
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
        }
    }
}
