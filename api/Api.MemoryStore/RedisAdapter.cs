using Api.Domain;
using StackExchange.Redis;

namespace Api.MemoryStore
{
    public class RedisAdapter : IMemoryStore
    {
        private readonly IConnectionMultiplexer _connection;

        public RedisAdapter(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }
        
        public ValueType Get(int value)
        {
            var db = _connection.GetDatabase();
            
            string storedValue = db.StringGet(value.ToString());

            if (storedValue == null)
            {
                return null;
            }
            
            return new ValueType(){Id = value, Value = storedValue};
        }

        public void Save(ValueType valueType)
        {
            var db = _connection.GetDatabase();

            db.StringSet(valueType.Id.ToString(), valueType.Value);
        }
    }
}