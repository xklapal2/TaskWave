using TaskWave.Domain.Entities;

namespace TaskWave.Api.Contracts.Users;

public record ListUserResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email
);

public static class ListUserResponseExtensions
{
    public static ListUserResponse ToDto(this User user)
        => new(
                user.Id.ToString(),
                user.FirstName,
                user.LastName,
                user.Email
            );
}