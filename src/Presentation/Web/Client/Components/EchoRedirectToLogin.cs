namespace AdventEcho.Presentation.Web.Client.Components;

public class EchoRedirectToLogin : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo($"account/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}", forceLoad: true);
    }
}