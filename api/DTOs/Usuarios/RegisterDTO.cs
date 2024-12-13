﻿using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Usuarios;

public class RegisterDTO {
	[Required]
	public string Nome { get; set; } = string.Empty;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Senha { get; set; } = string.Empty;
}
