using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace AdventEcho.Presentation.Web.Client.Components.Forms;

public partial class EchoForm<TModel> : EchoComponentBase where TModel : class
{
    [Parameter, EditorRequired] public required TModel Model { get; set; }
    [Parameter, EditorRequired] public required IValidator<TModel> Validator { get; set; }
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }

    [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }
    [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }

}