using GeekSevenLabs.AdventEcho.Application.Services;
using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Kernel.Data;

namespace GeekSevenLabs.AdventEcho.Application.Districts.Create;

public class CreateDistrictCommandHandler(
    IDistrictRepository districtRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateDistrictCommand, DistrictId>
{

    public async Task<Result<DistrictId>> Handle(CreateDistrictCommand command, CancellationToken cancellationToken)
    {
        // Authorization
        var canCreateDistrict = await userContext.CurrentUserCanCreateDistrictAsync();
        if(!canCreateDistrict) return Result.Fail<DistrictId>("Unauthorized to create a district.");
        
        // Domain
        District district; 
        
        try
        {
            district = new District(command.Name!, command.PastorId!.Value);
        }
        catch (DomainException domainException)
        {
            return Result.Fail<DistrictId>(domainException.Message);
        }
        
        districtRepository.Add(district);
        return await unitOfWork.CommitAsync(cancellationToken) ?
            Result.Ok(district.Id) :
            Result.Fail<DistrictId>("Failed to create district.");
    }
}
