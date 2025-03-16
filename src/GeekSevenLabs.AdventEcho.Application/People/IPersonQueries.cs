using GeekSevenLabs.AdventEcho.Application.Shared.People;

namespace GeekSevenLabs.AdventEcho.Application.People;

public interface IPersonQueries
{
    Task<PersonDto?> GetByDocumentAsync(string number);
}