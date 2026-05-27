namespace WizCo.Presentation.WebAPI.DTOs;

public class UsuarioTokenDTO
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}