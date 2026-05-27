using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WizCo.Core.Domain.Interfaces;
using WizCo.Presentation.WebAPI.DTOs;

namespace WizCo.Presentation.WebAPI.Controllers;

[ApiController]
[AllowAnonymous]
[EnableRateLimiting("Auth")]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticateService _authenticationService;
    private readonly IJwtTokenGenerator _jwt;
    
    public AuthController(IAuthenticateService authentication, IJwtTokenGenerator jwt)
    {
        _authenticationService = authentication ?? throw new ArgumentNullException(nameof(authentication));
        _jwt = jwt;
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<UsuarioTokenDTO>> Login([FromBody] LoginDTO userDTO) 
    {
        var result = await _authenticationService.Autenticar(userDTO.Email, userDTO.Senha);

        if (result)
        {
            var (token, expiration) = _jwt.GenerateToken(userDTO.Email);
            var tokenDTO = new UsuarioTokenDTO()
            {
                Token = token,
                Expiration = expiration
            };
            return Ok(tokenDTO);
        }

        ModelState.AddModelError("Erro", "Login Inválido!");
        return BadRequest(ModelState);
    }
    
    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar([FromBody] RegistroDTO userDTO) 
    {
        var result = await _authenticationService.RegistrarUsuario(userDTO.Email, userDTO.Senha);

        if(result)
            return Ok($"Usuário {userDTO.Email} criado com sucesso!");
        
        ModelState.AddModelError("Erro", "Login Inválido!");
        return BadRequest(ModelState);
    }
}