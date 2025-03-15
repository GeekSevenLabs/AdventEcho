namespace GeekSevenLabs.AdventEcho.Domain.People;

public record NameVo : ValueObject
{
    public NameVo(string first, string last)
    {
        First = first.TrimEnd();
        Last = last.TrimStart();

        // .IsNotNullOrEmpty(name.First, nameof(NameVo.First), FirstNameIsRequired)
        // .IsGreaterOrEqualsThan(name.First, 3, nameof(NameVo.First), FirstNameMinLength)
        // .IsLowerOrEqualsThan(name.First, 50, nameof(NameVo.First), FirstNameMaxLength)
        //
        // .IsNotNullOrEmpty(name.Last, nameof(NameVo.Last), LastNameIsRequired)
        // .IsGreaterOrEqualsThan(name.Last, 3, nameof(NameVo.Last), LastNameMinLength)
        // .IsLowerOrEqualsThan(name.Last, 100, nameof(NameVo.Last), LastNameMaxLength);
    }

    public string First { get; private init; }
    public string Last { get; private init; }
    
    public string FullName => $"{First} {Last}";
}