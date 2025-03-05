using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GeekSevenLabs.AdventEcho.Infrastructure.Identity.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    
    public EmailSender(
        IOptions<AuthMessageSenderOptions> optionsAccessor,
        ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }
    
    private AuthMessageSenderOptions Options { get; }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new InvalidOperationException("SendGridKey is not set.");
        }
        await Execute(Options.SendGridKey, subject, htmlMessage, email);
    }
    
    private async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("auth@adventecho.geekseven.com.br", "Advent Echo Security Team");
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
        
        _logger.LogInformation(log);

#if DEBUG
        _logger.LogInformation("Email sent({Subject}): TO {Email} : Message [{Message}]", subject, toEmail, message);
#endif
    }
}