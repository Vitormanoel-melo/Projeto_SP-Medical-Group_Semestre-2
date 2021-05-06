using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()

                .AddNewtonsoftJson(options => 
                {
                    // Ignora loops infinitos
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    // Ignora valores nulos
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services
                .AddAuthentication(options => 
                {
                    options.DefaultAuthenticateScheme   = "JwtBearer";
                    options.DefaultChallengeScheme      = "JwtBearer";
                })
                
                .AddJwtBearer("JwtBearer", options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,

                        ValidateAudience = true,

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("spMed-key-autentication")),

                        ClockSkew = TimeSpan.FromMinutes(30),

                        ValidIssuer = "SpMed.webApi",

                        ValidAudience = "SpMed.webApi"
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Habilita autenticação
            app.UseAuthentication();

            // Habilita autorização
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
