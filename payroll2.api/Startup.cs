using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Payroll2.Api.Configuration;
using Payroll2.Api.DataAccess;
using Payroll2.Api.Environment.DataAccess;
using Payroll2.Api.Environment.Services;
using Payroll2.Api.Framework;
using Payroll2.Api.Framework.Dependency;
using Payroll2.Api.Services;

namespace Payroll2.Api
{
    public class Startup
    {
        private const string CorsPolicy = "CorsPolicy";
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IServiceProvider ServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.Configure<JwtSecurityKey>(opt => _configuration.GetSection("JwtSecurityKey").Bind(opt));
            services.Configure<EnvironmentStringKey>(opt =>
                _configuration.GetSection("EnvironmentStringKey").Bind(opt));

            services.AddAuthentication(ConfigureAuthentication)
                .AddJwtBearer(ConfigureJwt);

            services.AddCors(o => o.AddPolicy(CorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Bootstrapper.Initialize(services, _configuration, new IBootstrapperExtension[]
            {
                new FrameworkBootstrapperExtension(),
                new EnvironmentDataAccessBootstrapperExtension(),
                new DataAccessBootstrapperExtension(),
                new EnvironmentServicesBootstrapperExtension(),
                new ServicesBootstrapperExtension(),
                new MapperBootstrapperExtension()
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsPolicy);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCustomMiddleware();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            ServiceProvider = app.ApplicationServices;
        }

        private void ConfigureJwt(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = FrameworkConstants.PayrollIssuer,
                ValidAudience = FrameworkConstants.PayrollAudience,
                IssuerSigningKeyResolver = ResolveKey
            };

            options.IncludeErrorDetails = false;
        }

        private IEnumerable<SecurityKey> ResolveKey(string token, SecurityToken securityToken, string kid,
            TokenValidationParameters validationParameters)
        {
            var key = ServiceProvider.GetService<IAppSettingsService>().GetJwtSecurityKey();
            return new[]
            {
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        }

        private static void ConfigureAuthentication(AuthenticationOptions options)
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}