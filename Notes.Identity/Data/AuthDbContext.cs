using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Identity.Data {
	public class AuthDbContext : IdentityDbContext<AppUser>{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder) { 
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));
			modelBuilder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
			modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));
			modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaim"));
			modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
			modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));
			modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));

			modelBuilder.ApplyConfiguration(new AppUserConfiguration());
		}
	}
}
