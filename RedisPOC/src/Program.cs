using StackExchange.Redis;

namespace RedisPOC
{
    class Program {
        static void Main(string[] args)
        {
            // connect
            string connectionString = System.Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
            IDatabase redisDatabase = redis.GetDatabase();
            // set
            redisDatabase.StringSet("foo", "bar");
            // get
            System.Console.WriteLine(redisDatabase.StringGet("foo"));
        }
    }
}
