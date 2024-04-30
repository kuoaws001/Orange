namespace WebApp.Data
{
    public interface IWebApiExecuter
    {
        Task<T?> InvokeGet<T>(string url);
        Task<T?> InvokePost<T>(string url, T obj);
        Task InvokePut<T>(string relativeUrl, T obj);
        Task InvokeDelete(string url);
    }
}
