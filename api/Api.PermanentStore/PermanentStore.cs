using System.Data;
using System.Linq;
using Api.Domain;
using Dapper;
using ValueType = Api.Domain.ValueType;

namespace Api.PermanentStore
{
    public class PermanentStore : IPermanentStore
    {
        private readonly IDbConnection _db;

        public PermanentStore(IDbConnection db)
        {
            _db = db;
        }
        
        public ValueType Get(int value)
        {   
            const string sql = "Select * from ValuesTable where Id = @Id;";

            return _db.Query<ValueType>(sql, new {Id = value}).SingleOrDefault();           
        }

        public void Save(ValueType valueType)
        {   
            const string sql = "INSERT INTO ValuesTable (Id, Value) Values (@Id, @Value);";

            _db.Execute(sql, valueType);
        }
    }
}