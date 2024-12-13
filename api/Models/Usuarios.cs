using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class Usuarios : IdentityUser{
	public string DisplayName { get; set; }

}
