namespace GeekSevenLabs.AdventEcho.Domain.People;

public record NameVo : ValueObject
{
    public NameVo(string first, string last)
    {
        First = first;
        Last = last;
        
        AddNotifications(new NameVoValidationContract(this));
    }

    public string First { get; private init; }
    public string Last { get; private init; }
}