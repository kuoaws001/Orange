
namespace WebApp.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private const string apiName = "ShirtsApi";

        private readonly IHttpClientFactory httpClientFactory;

        public WebApiExecuter(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T?> InvokeGet<T>(string url)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);

            return await httpClient.GetFromJsonAsync<T>(url);

            //var request = new HttpRequestMessage(HttpMethod.Get, url);
            //var response = await httpClient.SendAsync(request);

            //return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}


// Nuget Microsoft.Extensions.Http