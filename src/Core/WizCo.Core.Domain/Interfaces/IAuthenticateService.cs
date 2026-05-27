namespace WizCo.Core.Domain.Interfaces;

public interface IAuthenticateService
{
    Task<bool> Autenticar(string email, string password);
    Task<bool> RegistrarUsuario(string email, string password);
    Task Logout();
}