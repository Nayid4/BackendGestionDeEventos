using System.Reflection;

namespace WebAPI.Servicios
{
    public class PresentationAssemblyReference
    {
        internal static readonly Assembly assembly = typeof(PresentationAssemblyReference).Assembly;
    }
}
