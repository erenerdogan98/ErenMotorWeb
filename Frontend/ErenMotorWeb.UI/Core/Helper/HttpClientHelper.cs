namespace ErenMotorWeb.UI.Core.Helper
{
    public class HttpClientHelper(IHttpClientFactory httpClientFactory)
    {
        public HttpClient CreateClient(string clientName = "ApiClient") => httpClientFactory.CreateClient(clientName);
    }
}
