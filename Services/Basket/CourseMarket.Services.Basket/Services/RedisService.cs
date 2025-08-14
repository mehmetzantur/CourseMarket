using StackExchange.Redis;

namespace CourseMarket.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly int _dbIndex;
        private ConnectionMultiplexer _connectionMultiplexer;

        public int DbIndex => _dbIndex;

        public RedisService(string host, int port, int dbIndex = 1)
        {
            _host = host;
            _port = port;
            _dbIndex = dbIndex;
        }

        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase GetDb() => _connectionMultiplexer.GetDatabase(_dbIndex);

    }
}
