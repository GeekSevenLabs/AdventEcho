using MudBlazor.Utilities;

namespace AdventEcho.Presentation.Web.Client.Components.Pages;

public partial class EchoPage : EchoComponentBase
{
    private string CssClassContainer => CssBuilder.Default(Class).Build();
    
    [Parameter, EditorRequired] public required string Title { get; set; }
    [Parameter] public string? BackLink { get; set; }
    
    [Parameter] public MaxWidth MaxWidth { get; set; } = MaxWidth.Medium;
    
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
}