using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application;

public class EmailSender : IEmailSender<IdentityUser>
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }
    
    public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {   
        _logger.LogInformation("The {User} sends this confirmation link: {Code}", email, confirmationLink);
        
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        _logger.LogInformation("The {User} sends this reset link: {Code}", email, resetLink);
        
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        _logger.LogInformation("The {User} sends this reset code: {Code}", email, resetCode);
        
        return Task.CompletedTask;
    }
}