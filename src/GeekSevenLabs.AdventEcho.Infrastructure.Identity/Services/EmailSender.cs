using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resend;
using SendGrid;
using SendGrid.Helpers.Mail;
using EmailAddress = SendGrid.Helpers.Mail.EmailAddress;

namespace GeekSevenLabs.AdventEcho.Infrastructure.Identity.Services;

public class EmailSender(
    IOptions<AuthMessageSenderOptions> optionsAccessor,
    IResend resend,
    ILogger<EmailSender> logger) : IEmailSender
{
    private const string FromEmail = "auth@adventecho.geekseven.com.br";
    private const string FromName = "Advent Echo Security Team";

    private AuthMessageSenderOptions Options { get; } = optionsAccessor.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await ExecuteWithResend(subject, htmlMessage, email);
    }

    private async Task ExecuteWithResend(string subject, string message, string toEmail)
    {
        var msg = new EmailMessage
        {
            From = FromEmail
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

    private async Task Execute(string subject, string message, string toEmail)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new InvalidOperationException("SendGridKey is not set.");
        }

        var client = new SendGridClient(Options.SendGridKey);

        var from = new EmailAddress(FromEmail, FromName);
        var to = new EmailAddress(toEmail);

        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(msg);

        var content = await response.Body.ReadAsStringAsync();

        var log = response.IsSuccessStatusCode
            ? $"Email to {toEmail} queued successfully!"
            : $"Failure Email to {toEmail} - {content}";

        logger.LogInformation(log);

#if DEBUG
        logger.LogInformation("Email sent({Subject}): TO {Email} : Message [{Message}]", subject, toEmail, message);
#endif
    }
}