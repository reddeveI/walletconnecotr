using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Queries.GetAccountInfo;

namespace WalletConnector.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly ILogger _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost, Route("common/user/find")]
        public async Task<AccountInfoVm> GetAccountInfo(GetAccountInfoQuery query)
        {
            return await Mediator.Send(query);
        }


    }
}
