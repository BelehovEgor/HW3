using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Hosting;
using ServerAdapter.Commands;
using ServerAdapter.Commands.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Models.Request;
using Microsoft.Extensions.Logging;
using ServerAdapter.Logger;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ServerAdapter
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
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username("service1");
                        host.Password("service1");
                    });
                });

                x.AddRequestClient<GetUsersRequest>(new Uri("rabbitmq://localhost/user"));
                x.AddRequestClient<GetUserRequest>(new Uri("rabbitmq://localhost/user"));
                x.AddRequestClient<DeleteUserRequest>(new Uri("rabbitmq://localhost/userdelete"));
                x.AddRequestClient<PostUserRequest>(new Uri("rabbitmq://localhost/userpost"));
                x.AddRequestClient<PutUserRequest>(new Uri("rabbitmq://localhost/userput"));

                x.AddRequestClient<GetBuildingRequest>(new Uri("rabbitmq://localhost/building"));
                x.AddRequestClient<GetBuildingsRequest>(new Uri("rabbitmq://localhost/building"));
                x.AddRequestClient<DeleteBuildingRequest>(new Uri("rabbitmq://localhost/buildingdelete"));
                x.AddRequestClient<PostBuildingRequest>(new Uri("rabbitmq://localhost/buildingpost"));
                x.AddRequestClient<PutBuildingRequest>(new Uri("rabbitmq://localhost/buildingput"));

                x.AddRequestClient<GetPhoneRequest>(new Uri("rabbitmq://localhost/phone"));
                x.AddRequestClient<GetPhonesRequest>(new Uri("rabbitmq://localhost/phone"));
                x.AddRequestClient<DeletePhoneRequest>(new Uri("rabbitmq://localhost/phonedelete"));
                x.AddRequestClient<PostPhoneRequest>(new Uri("rabbitmq://localhost/phonepost"));
                x.AddRequestClient<PutPhoneRequest>(new Uri("rabbitmq://localhost/phoneput"));
            });

            services.AddTransient<IPutCommand, PutCommand>();
            services.AddTransient<IGetUserCommand, GetUserCommand>();
            services.AddTransient<IGetUsersCommand, GetUsersCommand>();
            services.AddTransient<IPostCommand, PostCommand>();
            services.AddTransient<IDeleteCommand, DeleteCommand>();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
