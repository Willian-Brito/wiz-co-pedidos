namespace WizCo.Core.Domain.Interfaces;

public interface IJwtTokenGenerator
{
    (string, DateTime) GenerateToken(string email);
}