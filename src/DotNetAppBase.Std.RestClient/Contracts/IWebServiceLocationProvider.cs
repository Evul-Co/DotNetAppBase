namespace DotNetAppBase.Std.RestClient.Contracts
{
    public interface IWebServiceLocationProvider
    {
        IRestController Get(string type);
    }
}