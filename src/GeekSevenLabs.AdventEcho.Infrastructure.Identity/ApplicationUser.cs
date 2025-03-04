using GeekSevenLabs.AdventEcho.Domain;
using Microsoft.AspNetCore.Identity;

namespace GeekSevenLabs.AdventEcho.Infrastructure.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public PersonId? PersonId { get; set; }
}