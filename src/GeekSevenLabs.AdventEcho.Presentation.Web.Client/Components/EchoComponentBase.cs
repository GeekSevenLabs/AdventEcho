using Microsoft.AspNetCore.Components;

namespace GeekSevenLabs.AdventEcho.Presentation.Web.Client.Components;

public class EchoComponentBase : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = [];

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }
}