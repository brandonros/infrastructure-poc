using StackExchange.Redis;

namespace ConsolePOC
{
    class Program {
        static int Main(string[] args)
        {
            // connect
            string connectionString = System.Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
            IDatabase redisDatabase = redis.GetDatabase();
            // set
            redisDatabase.StringSet("foo", "bar");
            // get
            System.Console.WriteLine(redisDatabase.StringGet("foo"));
            // return
            return 0;
        }
    }
}
