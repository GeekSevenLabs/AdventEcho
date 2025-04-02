using System.Diagnostics.CodeAnalysis;

namespace AdventEcho.Presentation.Web.Client.Services.Results;

public interface IUiUtils
{
    string BusyMessage { get; }
    bool IsBusy { get; }
    event Action? BusyChanged;
    
    Task<Guid> ShowBusyAsync(string message = "Processing...");
    Task HideBusyAsync(Guid busyId);
    Task ShowSuccessAsync(string message = "Operation completed successfully");
    Task ShowErrorAsync(Exception ex);
    
    void NavigateTo([StringSyntax(StringSyntaxAttribute.Uri)] string uri);
}