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
            //return await httpClient.GetFromJsonAsync<T>(url);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(request);
            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string url, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PostAsJsonAsync<T>(url, obj);
            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string url, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PutAsJsonAsync(url, obj);
            await HandlePotentialError(response);
        }

        public async Task InvokeDelete(string url)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
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
