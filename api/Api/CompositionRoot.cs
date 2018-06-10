using System;
using System.Data;
using Api.Controllers;
using Api.Domain;
using Api.MemoryStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using MySql.Data.MySqlClient;
using StackExchange.Redis;

namespace Api
{
    public class CompositionRoot : IControllerActivator
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDbConnection _dbConnection;

        public CompositionRoot(IConnectionMultiplexer redisConnection, IDbConnection dbConnection)
        {
            _redisConnection = redisConnection;
            _dbConnection = dbConnection;
        }

        public object Create(ControllerContext context)
        {
            var type = context.ActionDescriptor.ControllerTypeInfo.AsType();
            
            if (type == typeof(ValuesController))
            {
                return new ValuesController(
                    new ValuesProcessor(new PermanentStore.PermanentStore(_dbConnection), 
                                        new RedisAdapter(_redisConnection)));
            }
            throw new Exception("Unknown controller");
        }

        public void Release(ControllerContext context, object controller)
        {
            ;
        }
    }
}