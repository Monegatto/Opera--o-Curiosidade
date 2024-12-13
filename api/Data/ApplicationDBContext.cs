using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;
//strinDADASD123%g
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

		builder.Entity<IdentityRole>().HasData(roles);
	}
}
