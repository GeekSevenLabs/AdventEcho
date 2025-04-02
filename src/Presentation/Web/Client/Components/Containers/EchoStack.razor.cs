using MudBlazor.Utilities;

namespace AdventEcho.Presentation.Web.Client.Components.Containers;

public partial class EchoStack : EchoComponentBase
{
    private string CssClass => CssBuilder
        .Default("tw:flex")
        .AddClass("tw:flex-row", Direction == EchoDirection.Row)
        .AddClass("tw:flex-col", Direction == EchoDirection.Column)
        .AddClass("tw:gap-0", Gap == EchoGap.None)
        .AddClass("tw:gap-2", Gap == EchoGap.Small) 
        .AddClass("tw:gap-6", Gap == EchoGap.Medium)
        .AddClass("tw:gap-8", Gap == EchoGap.Large)
        .AddClass("tw:items-start", Alignment == EchoAlignment.Start)
        .AddClass("tw:items-center", Alignment == EchoAlignment.Center)
        .AddClass("tw:items-end", Alignment == EchoAlignment.End)
        .AddClass("tw:items-stretch", Alignment == EchoAlignment.Stretch)
        .AddClass("tw:justify-start", Justify == EchoJustify.Start)
        .AddClass("tw:justify-center", Justify == EchoJustify.Center)
        .AddClass("tw:justify-end", Justify == EchoJustify.End)
        .AddClass("tw:justify-stretch", Justify == EchoJustify.Stretch)
        .AddClass("tw:justify-between", Justify == EchoJustify.Default)
        .AddClass(Class)
        .Build();
    
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
    
    [Parameter] public required EchoGap Gap { get; set; } = EchoGap.Small;
    [Parameter] public required EchoAlignment Alignment { get; set; } = EchoAlignment.Default;
    [Parameter] public required EchoJustify Justify { get; set; } = EchoJustify.Default;
    [Parameter] public required EchoDirection Direction { get; set; } = EchoDirection.Column;
    
}

public enum EchoJustify
{
    Default,
    Start,
    Center,
    End,
    Stretch,
    Between
}

public enum EchoAlignment
{
    Default,
    Start,
    Center,
    End,
    Stretch
}

public enum EchoDirection
{
    Row,
    Column
}

public enum EchoGap 
{
    None,
    Small,
    Medium,
    Large
}