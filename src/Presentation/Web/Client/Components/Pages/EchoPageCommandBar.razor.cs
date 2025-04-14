namespace AdventEcho.Presentation.Web.Client.Components.Pages;

public partial class EchoPageCommandBar : EchoComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter] public required EchoPage EchoPage { get; set; }
}