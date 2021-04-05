using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletConnector.Api.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ILogger _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
