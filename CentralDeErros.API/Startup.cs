using AutoMapper;
using CentralDeErros.API.Configuration;
using CentralDeErros.Core;
using CentralDeErros.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CentralDeErros.API
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
            services.AddControllers();

            services.AddDbContext<CentralDeErrosDbContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddIdentityConfiguration(Configuration);

           
            services.AddScoped<ErrorService>();
            services.AddScoped<EnvironmentService>();
            services.AddScoped<LevelService>();
            services.AddScoped<MicrosserviceService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<TokenGeneratorService>();

            services.AddScoped<UserService>();

            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Central De Erros");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
