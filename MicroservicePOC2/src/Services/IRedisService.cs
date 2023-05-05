namespace MicroservicePOC2.Services
{
  public interface IRedisService
  {
    Task<string> Get(string key);
  }
}
