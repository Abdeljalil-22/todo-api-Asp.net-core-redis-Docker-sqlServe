using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DemoDocker.Library.Service
{
    public class CacheService : ICacheService
    {


        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetDataAsync<T>(string key)
        {
            try
            {
                var data = await _cache.GetStringAsync(key);


                if (string.IsNullOrEmpty(data))
                {
                    return default;

                }
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception)
            {

                return default;
            }

        }

        public async Task<bool> RemoveDataAsync(string key)
        {
            try
            {
                var data = await _cache.GetStringAsync(key);


                if (!string.IsNullOrEmpty(data))
                {
                    _cache.RemoveAsync(key);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;

            }


        }

        public async Task<bool> SetDataAsync<T>(string key, T data)
        {
            try
            {
                var cacheEntryOptions = new DistributedCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                     .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                await _cache.SetStringAsync(key, JsonConvert.SerializeObject(data), cacheEntryOptions);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
