using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SimpleBotCore.Bot;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore
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
            string banco = Configuration["BancoDefault"];

            if (banco == "SQL")
            {
                string sqlStrConnection = Configuration["ConnectionStrings:DefaultConnection"];

                if (sqlStrConnection != null)
                {
                    services.AddSingleton<IUPerguntasRepository>(new PerguntasSqlRepository(sqlStrConnection));
                    services.AddSingleton<IUserProfileRepository>(new UserProfileSqlRepository(sqlStrConnection));
                }
                else
                {
                    services.AddSingleton<IUPerguntasRepository, PerguntasMockRepository>();
                    services.AddSingleton<IUserProfileRepository>(new UserProfileMockRepository());
                }
             
            }
            else if (banco == "Mongo")
            {
                string mongoStrConnection = Configuration["Bot:Mongo"];

                if (mongoStrConnection != null)
                {
                    MongoClient mongoClient = new MongoClient(mongoStrConnection);

                    services.AddSingleton<IUPerguntasRepository>(new PerguntasMongoRepository(mongoClient));
                    services.AddSingleton<IUserProfileRepository>(new UserProfileMongoRepository(mongoClient));
                }
                else
                {
                    services.AddSingleton<IUPerguntasRepository, PerguntasMockRepository>();
                    services.AddSingleton<IUserProfileRepository>(new UserProfileMockRepository());
                }
            }
            else
            {
                services.AddSingleton<IUPerguntasRepository, PerguntasMockRepository>();
                services.AddSingleton<IUserProfileRepository>(new UserProfileMockRepository());
            }      

            services.AddSingleton<IBotDialogHub, BotDialogHub>();
            services.AddSingleton<BotDialog, SimpleBot>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
