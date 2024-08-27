using TaskWave.Domain.Entities;

namespace TaskWave.Application.Users.Common;

public record UserResult(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email
)
{
    public static UserResult FromUser(User user)
    {
        return new UserResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email
        );
    }
}