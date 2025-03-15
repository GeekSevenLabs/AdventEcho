namespace GeekSevenLabs.AdventEcho.Presentation.Web.Validations;

public static class ValidationFilterExtensions
{
    public static void AddValidation<TModel>(this RouteHandlerBuilder builder) where TModel : class
    {
        builder.AddEndpointFilter<ValidationFilter<TModel>>();
    }
}