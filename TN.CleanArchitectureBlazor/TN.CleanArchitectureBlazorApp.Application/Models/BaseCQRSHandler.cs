using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.CleanArchitectureBlazorApp.Application.Framework;

namespace TN.CleanArchitectureBlazorApp.Application.Models
{
    public class BaseCQRSHandler
    {
        protected readonly ICustomHttpClient _client;

        public BaseCQRSHandler(ICustomHttpClient client)
        {
            _client = client;
        }
    }
}
