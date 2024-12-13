using api.DTOs.Usuarios;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using Microsoft.AspNetCore.Identity;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

[Route("api/usuarios")]
[ApiController]
public class UsuariosController : ControllerBase{
	private readonly UserManager<Usuarios> _usuarioManager;
	private readonly ITokenService _tokenService;
	private readonly SignInManager<Usuarios> _signInManager;
	public UsuariosController(UserManager<Usuarios> usuarioManager, ITokenService tokenService, SignInManager<Usuarios> signInManager) {
		_usuarioManager = usuarioManager;
		_tokenService = tokenService;
		_signInManager = signInManager;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginDTO loginDTO) {

		if(!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		var user = await _usuarioManager.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);

		if(user == null) {
			return Unauthorized("Email não encontrado no sistema");
		}

		var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Senha, false);

		if(!result.Succeeded) {
			return Unauthorized("Senha incorreta");
		}

		return Ok(
			new NewUserDTO {
				Nome = user.UserName,
				Email = user.Email,
				Token = _tokenService.CreateToken(user)
			}
		);
	}

	[HttpPost("register")]
	[Authorize]
	public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO) {
		try {
			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			
			var user = new Usuarios {
				UserName = registerDTO.Nome,
				Email = registerDTO.Email
			};

			var createdUser = await _usuarioManager.CreateAsync(user, registerDTO.Senha);

			if(createdUser.Succeeded) {
				var roleResult = await _usuarioManager.AddToRoleAsync(user, "User");
				if(roleResult.Succeeded) {
					return Ok(
						new NewUserDTO {
							Nome = user.UserName,
							Email = user.Email,
							Token = _tokenService.CreateToken(user)
						}
					);
				} else {
					return StatusCode(500, roleResult.Errors);
				}
			} else {
				return StatusCode(500, createdUser.Errors);
			}
		}catch(Exception e) {
				return StatusCode(500, e.Message);
		}
	}
}

