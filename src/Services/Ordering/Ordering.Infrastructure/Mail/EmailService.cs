using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Intrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace Ordering.Infrastructure.Mail;

public sealed class EmailService : IEmailService
{
    private EmailSettings _emailSettings;
    private ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var subject = email.Subject;
        var body = email.Body;
        var to = new EmailAddress(email.To);

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to , subject, body, body);
        var response = await client.SendEmailAsync(sendGridMessage);

        _logger.LogInformation("Email sent.");

        if (response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK)
        {
            return true;
        }

        _logger.LogError("Email sending failed.");

        return false;
    }
}