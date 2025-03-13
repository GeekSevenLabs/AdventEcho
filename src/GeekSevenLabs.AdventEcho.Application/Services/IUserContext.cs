namespace GeekSevenLabs.AdventEcho.Application.Services;

public interface IUserContext
{
    Task<bool> CurrentUserCanCreateDistrictAsync();
}