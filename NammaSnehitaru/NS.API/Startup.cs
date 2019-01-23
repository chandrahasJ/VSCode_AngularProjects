using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NS.API.Data;
using NS.API.Helpers;

namespace NS.API
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
            services.AddDbContext<DataContext>(sqlite
                => sqlite.UseSqlite(
                    Configuration.GetConnectionString("ConnectionString")
                ));
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    //Stop Self referencing Loop detection in the Application
                    .AddJsonOptions(options => {
                        options.SerializerSettings.ReferenceLoopHandling = 
                                ReferenceLoopHandling.Ignore;
                    });

            //For Adding Seed Data
            services.AddTransient<Seed>();
            // Adding Cross Origin Resources
            services.AddCors();
            //Adding dependency Injections
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IDatingRepository,DatingRepository>();     
            //Add AutoMapper
            services.AddAutoMapper();       
            //Adding Authenication 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Configuration.GetSection("AppSettings:Token").Value
            ));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(
                        options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = false,
                                IssuerSigningKey = key,
                                ValidateIssuer = false,
                                ValidateAudience = false
                            };
                        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
                app.UseExceptionHandler(
                    builder =>
                    {
                        //Call the Application Builder.Run 
                        builder.Run(
                            //From where we can access context 
                            async context =>
                            {
                                //Modify the Status Code
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                                //Get Error Message
                                var error = context.Features.Get<IExceptionHandlerFeature>();
                                if (error != null)
                                {
                                    //Adding Custom Header to Response
                                    context.Response.AddCustomHeader(error.Error.Message);
                                    //Write the Error Message 
                                    await context.Response.WriteAsync(error.Error.Message);
                                }
                            }
                        );
                    }
                );
            }

            //Initialize Seeding 
            //seeder.SeederUser();
            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
