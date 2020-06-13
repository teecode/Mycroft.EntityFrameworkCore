using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Mycroft.EntityFrameworkCore.API.Configuration;
using Mycroft.EntityFrameworkCore.Data;
using Mycroft.EntityFrameworkCore.Data.IRepository;
using Mycroft.EntityFrameworkCore.Data.IRepository.Admin;
using Mycroft.EntityFrameworkCore.Data.IRepository.Approval;
using Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Cache;
using Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Logger;
using Mycroft.EntityFrameworkCore.Data.Repository.Admin;
using Mycroft.EntityFrameworkCore.Data.Repository.Approval;
using Mycroft.EntityFrameworkCore.Data.Repository.Utility;
using Mycroft.EntityFrameworkCore.Data.Repository.Utility.Cache;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Text;

namespace Mycroft.EntityFrameworkCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();
                    //builder.WithOrigins("http://localhost:3000",
                    //                    "http://localhost");
                });
            });

            //uncomment to set up fluentValidation

            //services
            // .AddMvc().ConfigureApiBehaviorOptions(options =>
            // {
            //     options.InvalidModelStateResponseFactory = c =>
            //     {
            //         var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
            //            .SelectMany(v => v.Errors)
            //            .Select(v => v.ErrorMessage));

            //         return new BadRequestObjectResult(new Response<List<string>>
            //         {
            //             data = errors.Split('\n').ToList(),
            //             message = errors.Replace("\n", " || ")
            //         });
            //     };
            // })
            // .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TerminalValidator>());

            services.AddMemoryCache();
            services.AddDbContext<DataEngineDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DataEngine_Connectionstring")));
            services.AddScoped<ICacheManager, CacheManager>();

            services.AddScoped<ILoggerManager, LoggerManager>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            #region Approval

            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IApprovalConfigurationRepository, ApprovalConfigurationRepository>();
            services.AddTransient<IApprovalRepository, ApprovalRepository>();

            #endregion Approval

            #region Admin

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminLoginRepository, AdminLoginRepository>();

            #endregion Admin

            services.AddScoped<IUnitofWork, UnitofWork>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOpenApiDocument(document =>
            {
                document.Title = "Mycroft BootStrapper";
                document.DocumentName = "Mr Mycroft";
                document.Description = "Oluwatimilehin Oluwamayowa";
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
                document.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header
                }));

                // document.OperationProcessors.Add(new AddRequiredHeaderParameter());
            });

            var settingsections = Configuration.GetSection("AppSettings");
            var keySection = settingsections.Get<AppSettings>();

            var key = Encoding.UTF8.GetBytes(keySection.Tokenkey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddCookie("Cookie")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
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

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}