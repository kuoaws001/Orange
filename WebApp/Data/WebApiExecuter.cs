using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApp.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private const string apiName = "ShirtsApi";
        private const string authApiName = "AuthorityApi";

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public WebApiExecuter(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<T?> InvokeGet<T>(string url)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            //return await httpClient.GetFromJsonAsync<T>(url);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(request);
            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string url, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);

            var response = await httpClient.PostAsJsonAsync<T>(url, obj);
            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string url, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);

            var response = await httpClient.PutAsJsonAsync(url, obj);
            await HandlePotentialError(response);
        }

        public async Task InvokeDelete(string url)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);

            var response = await httpClient.DeleteAsync(url);
            await HandlePotentialError(response);
        }

        private async Task HandlePotentialError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new WebApiException(errorJson);
            }
        }

        private async Task AddJwtToHeader(HttpClient httpClient)
        {
            var clientId = configuration.GetValue<string>("ClientId");
            var secret = configuration.GetValue<string>("Secret");

            // Authenticate
            var authClient = httpClientFactory.CreateClient(authApiName);
            var response = await authClient.PostAsJsonAsync("auth", new AppCredential
            {
                ClientId = clientId,
                Secret = secret
            });

            response.EnsureSuccessStatusCode();

            // get the jwt
            string strToken = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(strToken);

            // pass the jwt to endpoints through the http headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
    }
}


// Nuget Microsoft.Extensions.Http


//{
//    "title": "One or more validation errors occurred.",
//    "status": 404,
//    "errors": {
//        "ShirtId": [
//            "Shirt doesn't exist."
//        ]
//    }
//}
