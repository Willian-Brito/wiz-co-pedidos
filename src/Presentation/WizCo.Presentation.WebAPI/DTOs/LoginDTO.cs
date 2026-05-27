using System.ComponentModel.DataAnnotations;

namespace WizCo.Presentation.WebAPI.DTOs;

public class LoginDTO
{
    [EmailAddress(ErrorMessage = "Email Inválido!")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a Senha")]
    [StringLength(20, ErrorMessage = "O {0} deve ter pelo menos {2} e {1} caracteres longos", MinimumLength = 8)]
    public string Senha { get; set; } = string.Empty;
}