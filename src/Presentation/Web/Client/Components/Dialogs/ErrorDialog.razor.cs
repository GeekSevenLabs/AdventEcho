namespace AdventEcho.Presentation.Web.Client.Components.Dialogs;

public partial class ErrorDialog : ComponentBase
{
    [CascadingParameter] public required IMudDialogInstance MudDialog { get; set; }
    [Parameter] public required Exception[] Exceptions { get; set; }
    [Parameter] public required IEchoError[] Errors { get; set; }

    private Dictionary<string, string[]> GetProblems()
    {
        // return Exception is not ProblemDetailsException problem ? [] : problem.Errors;
        return [];
    }
    
    private bool IsProblemDetails() => false;
    
    private void Close() => MudDialog.Close();
    
}