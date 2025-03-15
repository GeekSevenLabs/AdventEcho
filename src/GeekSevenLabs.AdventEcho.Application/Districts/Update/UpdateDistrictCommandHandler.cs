using GeekSevenLabs.AdventEcho.Domain.Districts;

namespace GeekSevenLabs.AdventEcho.Application.Districts.Update;

public class UpdateDistrictCommandHandler(
    IDistrictRepository districtRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateDistrictCommand>
{
    public async Task<Result> Handle(UpdateDistrictCommand command, CancellationToken cancellationToken)
    {
        // Authorization
        var canCreateDistrict = await userContext.CurrentUserCanCreateDistrictAsync();
        if(!canCreateDistrict) return Result.Fail<DistrictId>("Unauthorized to create a district.");
        
        // Domain
        var district = await districtRepository.GetAsync(command.Id.Required());
        if(district is null) return Result.Fail<DistrictId>("District not found.");
        
        try
        {
            district.Update(command.Name!, command.PastorId.Required());
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