using api.Data;
using api.DTOs.Cadastros;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CadastrosRepository : ICadastroRepository {
	private readonly ApplicationDBContext _context;
	public CadastrosRepository(ApplicationDBContext context) {
		_context = context;
	}
	public async Task<Cadastros> CreateAsync(Cadastros cadastroModel) {
		await _context.Cadastros.AddAsync(cadastroModel);
		await _context.SaveChangesAsync();
		return cadastroModel;
	}

	public async Task<Cadastros?> DeleteAsync(int id) {
		var cadastroModel = await _context.Cadastros.FirstOrDefaultAsync(x => x.Id == id);
		if(cadastroModel == null) {
			return null;
		}
		
		_context.Cadastros.Remove(cadastroModel);
		await _context.SaveChangesAsync();
		return cadastroModel;
	}

	public async Task<List<Cadastros>> GetAllAsync(string? query) {
		var cadastros = _context.Cadastros.AsQueryable();
		if(!string.IsNullOrEmpty(query)) {
			cadastros = cadastros.Where(x => x.Nome.Contains(query));
		}
		return await cadastros.ToListAsync();
	}

	public async Task<Cadastros?> UpdateAsync(int id, UpdateCadastroDTO cadastroDTO) {
		var existingCadastro = await _context.Cadastros.FirstOrDefaultAsync(x => x.Id == id);
		if(existingCadastro == null) {
			return null;
		}

		existingCadastro.Nome = cadastroDTO.Nome;
		existingCadastro.Idade = cadastroDTO.Idade;
		existingCadastro.Email = cadastroDTO.Email;
		existingCadastro.Endereco = cadastroDTO.Endereco;
		existingCadastro.Informacoes = cadastroDTO.Informacoes;
		existingCadastro.Ativo = cadastroDTO.Ativo;
		existingCadastro.Interesses = cadastroDTO.Interesses;
		existingCadastro.Sentimentos = cadastroDTO.Sentimentos;
		existingCadastro.Valores = cadastroDTO.Valores;

		await _context.SaveChangesAsync();
		return existingCadastro;
	}
}
