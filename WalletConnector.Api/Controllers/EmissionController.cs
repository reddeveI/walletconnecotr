using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletConnector.Api.Controllers
{
    public class EmissionController : ControllerBase
    {
        private readonly ILogger _logger;

        public EmissionController(ILogger<EmissionController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
