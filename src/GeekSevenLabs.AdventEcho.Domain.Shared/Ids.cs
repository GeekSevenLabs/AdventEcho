using GeekSevenLabs.AdventEcho.Common;

namespace GeekSevenLabs.AdventEcho.Domain.Shared;

[StronglyTypedId] public readonly partial struct ChurchId : ITypedId;
[StronglyTypedId] public readonly partial struct DistrictId : ITypedId;
[StronglyTypedId] public readonly partial struct NoticeId : ITypedId;

[StronglyTypedId]
public readonly partial struct PersonId : ITypedId
{
    public static implicit operator Guid(PersonId id) => id.Value;
}
[StronglyTypedId] public readonly partial struct ScheduleDayAssignmentId : ITypedId;
[StronglyTypedId] public readonly partial struct ScheduleDayId : ITypedId;
[StronglyTypedId] public readonly partial struct ScheduleEventId : ITypedId;
[StronglyTypedId] public readonly partial struct ScheduleId : ITypedId;