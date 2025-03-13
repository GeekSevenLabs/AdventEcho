namespace GeekSevenLabs.AdventEcho.Application.Shared;

public static class StringsRoles
{
    public const string Developer = nameof(Developer);
    public const string DeveloperId = "b9a02c61-28ee-4b6e-acfb-fbd0b7d66639";
    public static readonly string DeveloperNormalized = Developer.ToUpper();
    
    public const string Administrator = nameof(Administrator);
    public const string AdministratorId = "c87aab47-2814-45d0-84e4-c824887e780e";
    public static readonly string AdministratorNormalized = Administrator.ToUpper();
    
    public const string Pastor = nameof(Pastor);
    public const string PastorId = "e98609b7-2d43-4fca-b041-b50d619ac490";
    public static readonly string PastorNormalized = Pastor.ToUpper();
    
    public const string Elder = nameof(Elder);
    public const string ElderId = "5ed933c0-8472-4f02-a17c-3d9eca44ac20";
    public static readonly string ElderNormalized = Elder.ToUpper();
    
    public const string Director = nameof(Director);
    public const string DirectorId = "8b00570d-7b91-4412-8f31-d200171806f8";
    public static readonly string DirectorNormalized = Director.ToUpper();
    
    public const string Member = nameof(Member);
    public const string MemberId = "3bf8bd1f-0db0-4462-b114-0d0900125eb6";
    public static readonly string MemberNormalized = Member.ToUpper();
    
    public static string Combine(params string[] roles)
    {
        return string.Join(",", roles);
    }
}