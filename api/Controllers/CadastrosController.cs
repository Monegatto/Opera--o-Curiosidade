using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.DTOs.Cadastros;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

[Route("api/cadastros")]
[ApiController]
public class CadastrosController : ControllerBase{
	private readonly ICadastroRepository _cadastroRepo;
	public CadastrosController(ICadastroRepository cadastroRepo) {
		_cadastroRepo = cadastroRepo;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] string? query) {

		if(!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		var cadastros = await _cadastroRepo.GetAllAsync(query);
		var cadastrosDTO = cadastros.Select(c => c.ToDTOFromModel()).ToList();
		return Ok(cadastrosDTO);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Create([FromBody] CreateCadastroDTO cadastroDTO) {
		
		if(!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		var cadastroModel = cadastroDTO.ToModelFromDTO();
		await _cadastroRepo.CreateAsync(cadastroModel);
		return Ok(cadastroModel.ToDTOFromModel());
	}

	[HttpPut]
	[Route("{id:int}")]
	[Authorize]
	public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCadastroDTO cadastroDTO) {

		if(!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		var cadastroModel = await _cadastroRepo.UpdateAsync(id, cadastroDTO);
		if(cadastroModel == null) {
			return NotFound();
		}
		return Ok(cadastroModel.ToDTOFromModel());
	}

	[HttpDelete]
	[Route("{id:int}")]
	[Authorize]
	public async Task<IActionResult> Delete([FromRoute] int id) {

		if(!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		var cadastroModel = await _cadastroRepo.DeleteAsync(id);
		if(cadastroModel == null) {
			return NotFound();
		}
		return NoContent();
	}
}