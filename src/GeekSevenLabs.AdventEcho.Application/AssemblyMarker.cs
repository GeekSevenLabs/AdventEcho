using System.Reflection;

namespace GeekSevenLabs.AdventEcho.Application;

public static class AssemblyMarker
{
    public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}