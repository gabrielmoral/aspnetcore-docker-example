using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using StackExchange.Redis;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var redisConnection = CreateRedisConnection();
            var mySqlConnection = CreateMySqlConnection();

            services.AddMvc();
            
            services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            services.AddSingleton<IControllerActivator>(new CompositionRoot(redisConnection,mySqlConnection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
        
        private static ConnectionMultiplexer CreateRedisConnection()
        {
            return ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("RedisConn"));
        }
        
        private static MySqlConnection CreateMySqlConnection(int times = 0)
        {
            const int retries = 10;

            try
            {
                return new MySqlConnection(Environment.GetEnvironmentVariable(("SqlConn")));
            }
            catch (Exception e)
            {
                if (times < retries)
                {
                    CreateMySqlConnection(times++);
                }

                throw new Exception("Cannot connect with the database");
            }
        }
    }
}
