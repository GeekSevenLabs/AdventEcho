using AdventEcho.Identity.Application.Shared.Accounts.Register;
using AdventEcho.Identity.Application.Shared.Services;
using AdventEcho.Presentation.Web.Client.Services.Results;

namespace AdventEcho.Presentation.Web.Client.Pages.Accounts;

public partial class RegisterPage : ComponentBase
{
    private readonly RegisterAccountRequest _request = new();
    private readonly RegisterAccountValidator _validator = new();
    
    [Inject] public required IAccountViewService AccountService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required ISnackbar Snackbar { get; set; }
    [Inject] public required IUiUtils Utils { get; set; }
    
    private async Task HandleValidSubmitAsync()
    {
        var result = await AccountService
            .RegisterAsync(_request, CancellationToken.None)
            .Use(Utils)
            .ShowBusy("Registering account...")
            .ShowError()
            .ShowSuccess("Account registered successfully.");

        await result.WhenSuccessAsync(GoToLoginAsync);
    }
    
    private async Task GoToLoginAsync()
    {
        await Task.CompletedTask;
        NavigationManager.NavigateTo("/account/login");
    }
}