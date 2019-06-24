using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using AutoMapper;

using Nanoblog.Api.Data;
using Nanoblog.Api.Services;
using Nanoblog.Api.Settings;
using Nanoblog.Api.Data.Models;
using Nanoblog.Api.Middleware;
using Nanoblog.Core.Data.Dto;

namespace Nanoblog
{
    public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", false);

			Configuration = builder.Build();

			CurrentEnvironment = env;
		}

		public IConfiguration Configuration { get; }

		private IHostingEnvironment CurrentEnvironment { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			if (CurrentEnvironment.IsDevelopment())
			{
				services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServer")));
			}

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IEntryService, EntryService>();
            services.AddTransient<ICommentService, CommentService>();
			services.AddTransient<IAccountService, AccountService>();

			services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddSingleton<IJwtHandler, JwtHandler>();

			services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

			services.AddCors();

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Entry, EntryDto>();
                cfg.CreateMap<Comment, CommentDto>();
            }, 
            AppDomain.CurrentDomain.GetAssemblies());

            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = jwtSettings.Issuer,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
				};
			});

			services.AddMvc()
                .AddJsonOptions(options =>
			    {
				    options.SerializerSettings.DateFormatString = "dd-MM-yyyy HH:mm:ss";
			    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
            if (env.IsProduction())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
			app.UseMiddleware<ErrorHandler>();
			app.UseAuthentication();

			app.UseCors(builder =>
			{
				builder.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
			});

			app.UseMvc();
		}
	}
}
