using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CountryDetails.IntermediateModels;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace CountryDetails.ExternalResources
{
    public class ExternalApiClient
    {
        public async Task<JToken> CreateRestClient(string requestUrl)
        {
            var client = new RestClient($"{requestUrl}");
            var request = new RestRequest();
            request.Method = Method.Get;

            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                return content;
            }
            return response.Content;
        }

        public async Task<IEnumerable<CountryAPIReturn>> GetCountryDetails(string countryCode)
        {
            var response = await CreateRestClient($"https://api.worldbank.org/v2/country/{countryCode}?format=json");
            bool hasContent = ((JProperty)((JObject)JsonConvert.DeserializeObject(response[0].ToString())).First)?.Name == "page";

            if (hasContent)
            {
                return JsonConvert.DeserializeObject<List<CountryAPIReturn>>(response[1].ToString());
            }

            return JsonConvert.DeserializeObject<List<CountryAPIReturn>>(response.ToString());
        }
    }
}