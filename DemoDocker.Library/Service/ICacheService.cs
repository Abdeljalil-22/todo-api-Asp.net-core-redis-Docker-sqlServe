
namespace DemoDocker.Library.Service
{
    public interface ICacheService
    {
        Task<T?> GetDataAsync<T>(string key);
        Task<bool> RemoveDataAsync(string key);
        Task<bool> SetDataAsync<T>(string key, T data);
    }
}