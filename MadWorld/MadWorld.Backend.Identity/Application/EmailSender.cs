using MadWorld.Backend.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application;

public class EmailSender : IEmailSender<IdentityUserExtended>
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }
    
    public Task SendConfirmationLinkAsync(IdentityUserExtended user, string email, string confirmationLink)
    {   
        _logger.LogInformation("The {User} sends this confirmation link: {Code}", email, confirmationLink);
        
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(IdentityUserExtended user, string email, string resetLink)
    {
        _logger.LogInformation("The {User} sends this reset link: {Code}", email, resetLink);
        
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(IdentityUserExtended user, string email, string resetCode)
    {
        _logger.LogInformation("The {User} sends this reset code: {Code}", email, resetCode);
        
        return Task.CompletedTask;
    }
}