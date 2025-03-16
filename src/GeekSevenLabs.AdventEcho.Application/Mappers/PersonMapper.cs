using GeekSevenLabs.AdventEcho.Application.Shared.People;
using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Application.Mappers;

public static class PersonMapper
{
    public static PersonDto? ToDto(this Person? person)
    {
        return person is null ? null : new PersonDto { Id = person.Id        };
    }

    public static async Task<PersonDto?> ToDto(this Task<Person?> person)
    {
        return (await person).ToDto();
    }
    
}