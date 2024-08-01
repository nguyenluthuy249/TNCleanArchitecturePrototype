using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IDateTimeService _dateTimeService;

        public EmailService(ILogger<EmailService> logger, IDateTimeService dateTimeService)
        {
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public string SendEmail(Email emailObject)
        {
            var message = $"Send email with subject {emailObject.Subject} and body {emailObject.Body} at {_dateTimeService.Now}";
            _logger.LogInformation(message);
            return message;
        }
    }
}
