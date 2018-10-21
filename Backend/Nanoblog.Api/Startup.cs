using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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

			services.AddTransient<IEntryService, EntryService>();
			services.AddTransient<IAccountService, AccountService>();

			services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddSingleton<IJwtHandler, JwtHandler>();

			services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

			services.AddCors();

			services.AddAutoMapper();

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

			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.DateFormatString = "dd-MM-yyyy HH:mm:ss";
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

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
