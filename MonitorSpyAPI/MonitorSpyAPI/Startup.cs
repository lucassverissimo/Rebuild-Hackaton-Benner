using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MonitorSpyAPI.Dominio;
using MonitorSpyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSpyAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            //configuração do mongodb
            services.Configure<MonitorStoreDatabaseSettings>(
                Configuration.GetSection(nameof(MonitorStoreDatabaseSettings)));
            //configuração do mongodb
            services.AddSingleton<IMonitorStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MonitorStoreDatabaseSettings>>().Value);

            services.AddSingleton<MonitorService>();
            services.AddSingleton<MonitorClickService>();
            services.AddSingleton<LogErroService>();

            services.AddControllers();
            services.AddCors();

            ConfigureSwagger(services);
        }

        private static void ConfigureSwagger(IServiceCollection services) {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1",
                    new OpenApiInfo {
                        Title = "Hackathon Benner 2021 Edition",
                        Version = "v1",
                        Description = "API para monitoramento de aplicações"
                    });
                //options.AddSecurityDefinition("token", new OpenApiSecurityScheme {
                //    In = ParameterLocation.Header,
                //    Description = "Autenticação de acesso aos endpoints",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey
                //});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "MonitorSpy");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
