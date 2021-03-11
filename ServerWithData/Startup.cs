using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using ServerWithData.Consumers;
using ServerWithData.DbEntity;
using ServerWithData.Mapper;
using ServerWithData.Mapper.Impl;
using ServerWithData.Repositories;
using ServerWithData.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData
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
            services.AddMassTransit(x =>
            {
                x.AddConsumer<GetUsersConsumer>();
                x.AddConsumer<GetUserConsumer>();
                x.AddConsumer<DeleteUserConsumer>();
                x.AddConsumer<PostUserConsumer>();
                x.AddConsumer<PutUserConsumer>();

                x.AddConsumer<GetBuildingsConsumer>();
                x.AddConsumer<GetBuildingConsumer>();
                x.AddConsumer<DeleteBuildingConsumer>();
                x.AddConsumer<PostBuildingConsumer>();
                x.AddConsumer<PutBuildingConsumer>();

                x.AddConsumer<GetPhonesConsumer>();
                x.AddConsumer<GetPhoneConsumer>();
                x.AddConsumer<DeletePhoneConsumer>();
                x.AddConsumer<PostPhoneConsumer>();
                x.AddConsumer<PutPhoneConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username("service2");
                        host.Password("service2");
                    });

                    cfg.ReceiveEndpoint("user", ep =>
                    {
                        ep.ConfigureConsumer<GetUsersConsumer>(context);
                        ep.ConfigureConsumer<GetUserConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("userdelete", ep =>
                    {
                        ep.ConfigureConsumer<DeleteUserConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("userpost", ep =>
                    {
                        ep.ConfigureConsumer<PostUserConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("userput", ep =>
                    {
                        ep.ConfigureConsumer<PutUserConsumer>(context);
                    });


                    cfg.ReceiveEndpoint("building", ep =>
                    {
                        ep.ConfigureConsumer<GetBuildingsConsumer>(context);
                        ep.ConfigureConsumer<GetBuildingConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("buildingdelete", ep =>
                    {
                        ep.ConfigureConsumer<DeleteBuildingConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("buildingpost", ep =>
                    {
                        ep.ConfigureConsumer<PostBuildingConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("buildingput", ep =>
                    {
                        ep.ConfigureConsumer<PutBuildingConsumer>(context);
                    });


                    cfg.ReceiveEndpoint("phone", ep =>
                    {
                        ep.ConfigureConsumer<GetPhonesConsumer>(context);
                        ep.ConfigureConsumer<GetPhoneConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("phonedelete", ep =>
                    {
                        ep.ConfigureConsumer<DeletePhoneConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("phonepost", ep =>
                    {
                        ep.ConfigureConsumer<PostPhoneConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("phoneput", ep =>
                    {
                        ep.ConfigureConsumer<PutPhoneConsumer>(context);
                    });
                });
            });

            services.AddDbContext<AppContext>();

            services.AddTransient<IMapper<User, DbUser>, UserMapper>();
            services.AddTransient<IMapper<Phone, DbPhone>, PhoneMapper>();
            services.AddTransient<IMapper<Building, DbBuilding>, BuildingMapper>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBuildingRepository, BuildingRepository>();
            services.AddTransient<IPhoneRepository, PhoneRepository>();

            services.AddMassTransitHostedService();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppContext>();
            context.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
