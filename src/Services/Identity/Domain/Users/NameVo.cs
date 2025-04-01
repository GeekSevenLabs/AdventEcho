namespace AdventEcho.Identity.Domain.Users;

[HasPrivateEmptyConstructor]
public partial class NameVo
{
    public static NameVo Empty => new();
    
    public NameVo(string first, string last)
    {
        First = first;
        Last = last;
    }

    public string First { get; private set; }
    public string Last { get; private set; }
    
    public static implicit operator string(NameVo name)
    {
        return $"{name.First} {name.Last}";
    }
}