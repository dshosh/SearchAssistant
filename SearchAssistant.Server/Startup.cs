using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchAssistant.Infra;
using SearchAssistant.Infra.Spiders;

namespace SearchAssistant.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            foreach (var section in Configuration.GetSection("Spiders").GetChildren())
            {
                services.AddHttpClient(section.Key, c =>
                {
                    c.BaseAddress = new Uri(section.GetValue<string>("BaseUrl"));
                    c.DefaultRequestHeaders.Add("User-Agent", section.GetValue<string>("UserAgent"));
                });
            }

            services.AddCors(o => o.AddPolicy("AnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped<ISpiderFactory, SpiderFactory>();
            services.AddScoped<ICanSearch, Dispatcher>();
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
            app.UseCors("AnyOrigin");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
