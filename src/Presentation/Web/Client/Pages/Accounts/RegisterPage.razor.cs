using AdventEcho.Identity.Application.Shared.Accounts.Register;

namespace AdventEcho.Presentation.Web.Client.Pages.Accounts;

public partial class RegisterPage : ComponentBase
{
    private readonly RegisterAccountRequest _request = new();
    private readonly RegisterAccountValidator _validator = new();
    
    private async Task HandleValidSubmitAsync()
    {
        await Task.CompletedTask;
    }
}