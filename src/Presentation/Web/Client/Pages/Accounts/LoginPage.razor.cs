using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Identity.Application.Shared.Services;
using AdventEcho.Presentation.Web.Client.Services.Results;

namespace AdventEcho.Presentation.Web.Client.Pages.Accounts;

public partial class LoginPage : ComponentBase
{
    private readonly LoginAccountValidator _validator = new();
    private readonly LoginAccountRequest _request = new();
    
    [SupplyParameterFromQuery] public string? ReturnUrl { get; set; }
    [Inject] public required IAccountViewService AccountService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required ISnackbar Snackbar { get; set; }
    [Inject] public required IUiUtils Utils { get; set; }
    
    private async Task HandleValidSubmitAsync()
    {
        _request.UseCookie = true;
        
        var result = await AccountService
            .LoginAsync(_request, CancellationToken.None)
            .Use(Utils)
            .ShowBusy("Entering your account...")
            .ShowError()
            .ShowSuccess("Welcome back!");
        
        
        await result.WhenSuccessAsync(GoToReturnUrlOrHomeAsync);
    }
    
    private async Task GoToReturnUrlOrHomeAsync(LoginAccountResponse response)
    {
        await Task.CompletedTask;
        NavigationManager.NavigateTo( ReturnUrl ?? "/", forceLoad: true);
    }
}