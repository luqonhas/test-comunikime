using ComunikiMe.Domain.Handlers.Authentications;
using ComunikiMe.Domain.Handlers.Carts;
using ComunikiMe.Domain.Handlers.Products;
using ComunikiMe.Domain.Handlers.Users;
using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Infra.Data.Contexts;
using ComunikiMe.Infra.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComunikiMe.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            // Adding CORS
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                );
            });

            // Adding Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComunikiMe.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

            // Connecting DB
            services.AddDbContext<ComunikiMeContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddAuthentication(options =>
                {
                    // Authentications
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })

                .AddJwtBearer("JwtBearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validations
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("comunikime-authentication-key")),
                        ClockSkew = TimeSpan.FromMinutes(30),
                        ValidIssuer = "comunikime",
                        ValidAudience = "comunikime"
                    };
                });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            // Dependencies Injection:
            #region Users
            services.AddTransient<IUserRepository, UserRepository>();

            // Commands:
            services.AddTransient<CreateUserHandle, CreateUserHandle>();
            services.AddTransient<DeleteUserHandle, DeleteUserHandle>();
            services.AddTransient<LoginEmailHandle, LoginEmailHandle>();
            services.AddTransient<LoginUserNameHandle, LoginUserNameHandle>();

            // Queries:
            services.AddTransient<ListUserHandle, ListUserHandle>();
            services.AddTransient<SearchUserByIdHandle, SearchUserByIdHandle>();
            services.AddTransient<SearchUserByEmailHandle, SearchUserByEmailHandle>();
            #endregion

            #region Products
            services.AddTransient<IProductRepository, ProductRepository>();

            // Commands:
            services.AddTransient<CreateProductHandle, CreateProductHandle>();
            services.AddTransient<DeleteProductHandle, DeleteProductHandle>();
            services.AddTransient<UpdateProductHandle, UpdateProductHandle>();

            // Queries:
            services.AddTransient<ListProductHandle, ListProductHandle>();
            services.AddTransient<SearchProductByIdHandle, SearchProductByIdHandle>();
            #endregion

            #region Carts
            services.AddTransient<ICartRepository, CartRepository>();

            // Commands:
            services.AddTransient<CreateCartHandle, CreateCartHandle>();
            services.AddTransient<DeleteCartHandle, DeleteCartHandle>();

            // Queries:
            services.AddTransient<ListCartHandle, ListCartHandle>();
            services.AddTransient<SearchCartByIdHandle, SearchCartByIdHandle>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComunikiMe.Api v1"));
            }

            app.UseRouting();

            // JWT
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
