namespace WebApp.Data
{
    public interface IWebApiExecuter
    {
        Task<T?> InvokeGet<T>(string url);
        Task<T?> InvokePost<T>(string url, T obj);
    }
}
