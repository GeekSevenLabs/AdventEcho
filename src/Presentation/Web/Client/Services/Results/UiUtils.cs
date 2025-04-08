using System.Diagnostics.CodeAnalysis;
using AdventEcho.Kernel.Exceptions;
using AdventEcho.Presentation.Web.Client.Components.Dialogs;

namespace AdventEcho.Presentation.Web.Client.Services.Results;

public class UiUtils(IDialogService dialogService, ISnackbar snackbar, NavigationManager navigation) : IUiUtils
{
    private const string DefaultSuccessMessage = "Operação realizada com sucesso";
    private const string DefaultBusyMessage = "Processando...";
    public event Action? BusyChanged;
    private readonly Dictionary<Guid, string> _busies = [];

    public bool IsBusy { get; private set; }
    public string BusyMessage { get; private set; } = DefaultBusyMessage;
    
    public async Task ShowErrorAsync(Exception ex)
    {
        var dialogParameters = new DialogParameters<ErrorDialog>
        {
            { d => d.Exceptions, [ex] },
            { d => d.Errors, [] }
        };
        await dialogService.ShowAsync<ErrorDialog>(ExtractFriendlyTitle(ex), dialogParameters);
    }

    public async Task ShowErrorAsync(IEchoError[] errors)
    {
        var dialogParameters = new DialogParameters<ErrorDialog>
        {
            { d => d.Exceptions, [] },
            { d => d.Errors, errors }
        };
        await dialogService.ShowAsync<ErrorDialog>("", dialogParameters);
    }

    public void NavigateTo([StringSyntax(StringSyntaxAttribute.Uri)] string uri) => navigation.NavigateTo(uri, forceLoad: true);

    private void RefreshBusyState()
    {
        var hasBusy = _busies.Count > 0;
        var busyMessage = _busies.LastOrDefault().Value ?? DefaultBusyMessage;
        
        if (hasBusy == IsBusy && busyMessage == BusyMessage) return;
        
        IsBusy = hasBusy;
        BusyMessage = busyMessage;
        BusyChanged?.Invoke();
    }

    public async Task<Guid> ShowBusyAsync(string message = DefaultBusyMessage)
    {
        await Task.CompletedTask;
        var busyId = Guid.NewGuid();
        _busies.Add(busyId, message);
        RefreshBusyState();
        return busyId;
    }

    public async Task HideBusyAsync(Guid busyId)
    {
        await Task.CompletedTask;
        _busies.Remove(busyId);
        RefreshBusyState();
    }

    public async Task ShowSuccessAsync(string message = DefaultSuccessMessage)
    {
        await Task.CompletedTask;
        snackbar.Add(message, Severity.Success);
    }
    
    private static string ExtractFriendlyTitle(Exception exception)
    {
        const string title = "Ops! An error occurred";
        return exception switch
        {
            ProblemDetailsException problemDetailsException => problemDetailsException.Title,
            HttpRequestException { Message: "TypeError: Failed to fetch" } => "Oops! No connection",
            _ => title
        };
    }
}