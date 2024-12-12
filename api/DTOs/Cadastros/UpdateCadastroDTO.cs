using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Cadastros;

public class UpdateCadastroDTO {
	[Required]
	public string Nome { get; set; } = string.Empty;

	public int Idade { get; set; }

	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	public string Endereco { get; set; } = string.Empty;

	public string Informacoes { get; set; } = string.Empty;

	[Required]
	public bool Ativo { get; set; }

	public string Interesses { get; set; } = string.Empty;

	public string Sentimentos { get; set; } = string.Empty;

	public string Valores { get; set; } = string.Empty;
}
