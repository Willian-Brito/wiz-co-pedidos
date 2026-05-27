using System.ComponentModel.DataAnnotations;

namespace WizCo.Presentation.WebAPI.DTOs;

public class RegistroDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "Email Inválido!")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "O {0} deve ter pelo menos {2} e {1} caracteres longos", MinimumLength = 8)]
    public string Senha { get; set; }
    
    [Required]
    [Display(Name = "Confirmar Senha")]
    [Compare("Senha", ErrorMessage = "As senhas não correspondem!")]
    public string ConfirmarSenha { get; set; }
}