using Flurl.Http;

namespace DotNetAppBase.Std.RestClient.Contracts
{
    public interface IRestController
    {
        int DefaultTimeout { get; }

        string BaseUrl();

        IFlurlRequest Intercept(IFlurlRequest withTimeout);
    }
}