using api.DTOs.Cadastros;
using api.Models;

namespace api.Mappers;

public static class CadastrosMapper {
	public static CadastroDTO ToDTOFromModel(this Cadastros cadastroModel) {
		return new CadastroDTO {
			Id = cadastroModel.Id,
			Nome = cadastroModel.Nome,
			Idade = cadastroModel.Idade,
			Email = cadastroModel.Email,
			Endereco = cadastroModel.Endereco,
			Informacoes = cadastroModel.Informacoes,
			Ativo = cadastroModel.Ativo,
			Interesses = cadastroModel.Interesses,
			Sentimentos = cadastroModel.Sentimentos,
			Valores = cadastroModel.Valores
		};
	}

	public static Cadastros ToModelFromDTO(this CreateCadastroDTO cadastroDTO) {
		return new Cadastros {
			Nome = cadastroDTO.Nome,
			Idade = cadastroDTO.Idade,
			Email = cadastroDTO.Email,
			Endereco = cadastroDTO.Endereco,
			Informacoes = cadastroDTO.Informacoes,
			Ativo = cadastroDTO.Ativo,
			Interesses = cadastroDTO.Interesses,
			Sentimentos = cadastroDTO.Sentimentos,
			Valores = cadastroDTO.Valores
		};
	}
}
