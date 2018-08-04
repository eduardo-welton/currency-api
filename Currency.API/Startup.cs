using Currency.API.Providers;
using Currency.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Currency.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IQuoteCurrencyService, QuoteCurrencyService>();
            services.AddSingleton<IQuoteProvider, CurrencyLayerProvider>();
            services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy",
                 builder => builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                                   .AllowCredentials()
                 );
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
