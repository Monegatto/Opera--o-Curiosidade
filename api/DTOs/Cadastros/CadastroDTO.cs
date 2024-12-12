namespace api.DTOs.Cadastros;

public class CadastroDTO {
	public int Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public int Idade { get; set; }
	public string Email { get; set; } = string.Empty;
	public string Endereco { get; set; } = string.Empty;
	public string Informacoes { get; set; } = string.Empty;
	public bool Ativo { get; set; }
	public string Interesses { get; set; } = string.Empty;
	public string Sentimentos { get; set; } = string.Empty;
	public string Valores { get; set; } = string.Empty;
}
