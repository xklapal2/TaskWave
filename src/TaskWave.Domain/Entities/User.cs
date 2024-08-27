using System.ComponentModel.DataAnnotations.Schema;

using TaskWave.Domain.Common.Abstractions;

namespace TaskWave.Domain.Entities;

public sealed class User : Entity
{
    [NotMapped]
    private readonly string _passwordHash = string.Empty;

    private User(string firstName, string lastName, string email, string passwordHash) : base(Ulid.NewUlid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public static User Create(string firstName, string lastName, string email, string passwordHash)
    {
        return new User(firstName, lastName, email, passwordHash);
    }

    private User() : base(Ulid.NewUlid())
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }
}