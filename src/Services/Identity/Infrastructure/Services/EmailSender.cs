using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Resend;

namespace AdventEcho.Identity.Infrastructure.Services;

public class EmailSender(IResend resend, ILogger<EmailSender> logger) : IEmailSender
{
    private const string FromEmail = "auth@adventecho.geekseven.com.br";
    private const string FromName = "Advent Echo Security Team";

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await ExecuteWithResend(subject, htmlMessage, email);
    }

    private async Task ExecuteWithResend(string subject, string message, string toEmail)
    {
        var msg = new EmailMessage
        {
            From = new EmailAddress { Email = FromEmail, DisplayName = FromName }
        };

        msg.To.Add(toEmail);
        msg.Subject = subject;
        msg.TextBody = message;
        msg.HtmlBody = message;

        var response = await resend.EmailSendAsync(msg);
        
        var log = response.Success
            ? $"(RESEND) ==> Email to {toEmail} queued successfully!"
            : $"(RESEND) ==> Failure Email to {toEmail} - {message}";

        logger.LogInformation(log);

#if DEBUG
        logger.LogInformation("Email sent({Subject}): TO {Email} : Message [{Message}]", subject, toEmail, message);
#endif
    }
}