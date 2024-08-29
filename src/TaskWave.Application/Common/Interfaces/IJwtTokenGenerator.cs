namespace TaskWave.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Ulid id, string firstName, string lastName, string email, List<string> permissions, List<string> roles);
}