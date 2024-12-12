using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDBContext : DbContext{
	public ApplicationDBContext(DbContextOptions option) : base(option){
		
	}

	public DbSet<Cadastros> Cadastros { get; set;}
}
