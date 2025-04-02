using AdventEcho.Presentation.Web.Client.Components.Containers;

namespace AdventEcho.Presentation.Web.Client.Components.Pages;

public partial class EchoPageSection : EchoComponentBase
{
    [Parameter] public string? Label { get; set; }
    [Parameter] public EchoGap Gap { get; set; } = EchoGap.Medium;
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
}