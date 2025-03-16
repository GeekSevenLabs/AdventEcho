using GeekSevenLabs.AdventEcho.Application.Mappers;
using GeekSevenLabs.AdventEcho.Application.People;
using GeekSevenLabs.AdventEcho.Application.Shared.People;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Contexts;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Queries;

public class PersonQueries(AdventEchoDbContext db) : IPersonQueries
{
    public async Task<PersonDto?> GetByDocumentAsync(string number)
    {
        return await db.People
            .AsNoTracking()
            .FirstOrDefaultAsync(person => person.Document.Number == number)
            .ToDto();
    }
}