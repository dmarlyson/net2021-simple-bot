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
                string sql = Configuration["ConnectionStrings:DefaultConnection"];

                if (sql != null)
                {
                    services.AddSingleton<IUPerguntasRepository>(new PerguntasSqlRepository(sql));
                    services.AddSingleton<IUserProfileRepository>(new UserProfileSqlRepository(sql));
                }
                else
                {
                    services.AddSingleton<IUPerguntasRepository, PerguntasMockRepository>();
                    services.AddSingleton<IUserProfileRepository>(new UserProfileMockRepository());
                }
             
            }
            else if (banco == "Mongo")
            {
                string mongoDb = Configuration["Bot:Mongo"];

                if (mongoDb != null)
                {
                    MongoClient mongo = new MongoClient(mongoDb);

                    services.AddSingleton<IUPerguntasRepository>(new PerguntasMongoRepository(mongo));
                    services.AddSingleton<IUserProfileRepository>(new UserProfileMongoRepository(mongo));
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
