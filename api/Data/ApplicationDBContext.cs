using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDBContext : IdentityDbContext<Usuarios>{
	public ApplicationDBContext(DbContextOptions option) : base(option){
		
	}

	public DbSet<Cadastros> Cadastros { get; set;}

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		List<IdentityRole> roles = new List<IdentityRole> {
			new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN" },
			new IdentityRole{ Name = "User", NormalizedName = "USER" }
		};

		var hasher = new PasswordHasher<IdentityUser>();
		builder.Entity<Usuarios>().HasData(new Usuarios {
			Id = "94efafbe-14cc-4c5f-a96d-aae43fd66591",
			DisplayName = "Luciano Henrique",
			UserName = "luciano@gmail.com",
			NormalizedUserName = "LUCIANO@GMAIL.COM",
			Email = "luciano@gmail.com",
			NormalizedEmail = "LUCIANO@GMAIL.COM",
			PasswordHash = hasher.HashPassword(null, "123")
			
		});

		builder.Entity<IdentityRole>().HasData(roles);
	}
}
