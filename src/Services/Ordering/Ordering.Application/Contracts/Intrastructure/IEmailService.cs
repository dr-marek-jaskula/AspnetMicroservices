using Ordering.Application.Models;

namespace Ordering.Application.Contracts.Intrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}