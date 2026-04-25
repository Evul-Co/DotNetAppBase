#region License

// Copyright(c) 2020 GrappTec
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

#endregion

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