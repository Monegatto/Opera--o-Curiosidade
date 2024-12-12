using api.DTOs.Cadastros;
using api.Models;

namespace api.Interfaces;

public interface ICadastroRepository {
	Task<List<Cadastros>> GetAllAsync(string? query);
	Task<Cadastros> CreateAsync(Cadastros cadastroModel);
	Task<Cadastros?> UpdateAsync(int id, UpdateCadastroDTO cadastroDTO);
	Task<Cadastros?> DeleteAsync(int id);
}
