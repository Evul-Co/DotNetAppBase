using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNetAppBase.Std.Library.Geo;

public static class GeoAddressResolver
{
    private const string DefaultUrl = "https://viacep.com.br/ws/{0}/json/";

    public static async Task<Result> AsyncQuery(string cep)
    {
        if (!XHelper.Data.Validations.IsCep(cep))
        {
            return null;
        }

        using var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(new Uri(string.Format(DefaultUrl, cep)));

        try
        {
            return JsonConvert.DeserializeObject<Result>(response);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static Result Query(string cep)
    {
        if (!XHelper.Data.Validations.IsCep(cep))
        {
            return null;
        }

        using var httpClient = new HttpClient();
        var response = httpClient.GetStringAsync(new Uri(string.Format(DefaultUrl, cep))).Result;

        try
        {
            return JsonConvert.DeserializeObject<Result>(response);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public class Result
    {
        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("gia")]
        public string Gia { get; set; }

        [JsonProperty("ibge")]
        public string Ibge { get; set; }

        [JsonProperty("localidade")]
        public string Localidade { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("unidade")]
        public string Unidade { get; set; }
    }
}