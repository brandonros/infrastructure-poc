namespace MicroservicePOC1.Services
{
  public interface IRedisService
  {
    Task<string> Get(string key);
  }
}
