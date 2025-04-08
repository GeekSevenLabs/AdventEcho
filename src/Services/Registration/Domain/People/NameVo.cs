namespace AdventEcho.Registration.Domain.People;

[HasPrivateEmptyConstructor]
public partial class NameVo
{
    public NameVo(string first, string last)
    {
        First = first.TrimStart().TrimEnd();
        Last = last.TrimStart().TrimEnd();
        
        Throw.When.NullOrEmpty(First, "First name cannot be empty.");
        Throw.When.NullOrEmpty(Last, "Last name cannot be empty.");
    }
    
    public string First { get; private set; }
    public string Last { get; private set; }
    
    public string FullName => $"{First} {Last}";
}