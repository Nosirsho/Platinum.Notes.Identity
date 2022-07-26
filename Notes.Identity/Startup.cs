using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Notes.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Models;

namespace Notes.Identity {
	public class Startup {
		public IConfiguration AppConfiguration { get; }
		public Startup(IConfiguration configuration) =>
			AppConfiguration = configuration;

		public void ConfigureServices(IServiceCollection services) {
			var connectionString = AppConfiguration.GetValue<string>("DbConnection");

			services.AddDbContext<AuthDbContext>(options => {
				options.UseSqlite(connectionString);
			});

			services.AddIdentity<AppUser, IdentityRole>(config => {
				config.Password.RequiredLength = 4;
				config.Password.RequireDigit = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireUppercase = false;
			})
				.AddEntityFrameworkStores<AuthDbContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddAspNetIdentity<AppUser>()
				.AddInMemoryApiResources(Configuration.ApiResources)
				.AddInMemoryIdentityResources(Configuration.IdentityResources)
				.AddInMemoryApiScopes(Configuration.ApiScopes)
				.AddInMemoryClients(Configuration.Clients)
				.AddDeveloperSigningCredential();
			services.ConfigureApplicationCookie(config => {
				config.Cookie.Name = "Notes.Identity.Cookie";
				config.LoginPath = "/Auth/Login";
				config.LoginPath = "/Auth/Logout";
			});
		}
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseIdentityServer();
			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
