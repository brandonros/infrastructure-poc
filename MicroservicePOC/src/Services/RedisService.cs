using StackExchange.Redis;

namespace MicroservicePOC.Services
{
  public class RedisService : IRedisService
  {
    private readonly ConnectionMultiplexer _redis;

    public RedisService(ConnectionMultiplexer redis)
    {
      _redis = redis;
    }

    public async Task<string> Get(string key)
    {
      var db = _redis.GetDatabase();
      return await db.StringGetAsync(key);
    }
  }
}
