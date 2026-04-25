using System.Reflection;
using System.Resources;

[assembly: AssemblyVersion("1.2.37")]
[assembly: AssemblyFileVersion("1.2.37")]
[assembly: SatelliteContractVersion("1.2.37")]

#if BETA
    [assembly: AssemblyInformationalVersion("BETA")]
#elif DEBUG
    [assembly: AssemblyInformationalVersion("1.2.37")]
#else
    [assembly: AssemblyInformationalVersion("1.2.37")]
#endif