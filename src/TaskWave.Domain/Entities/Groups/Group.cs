using TaskWave.Domain.Common.Abstractions;

namespace TaskWave.Domain.Entities.Groups;

public class Group : Entity
{
    public string Name { get; set; } = string.Empty;

    private readonly List<GroupMember> _members = [];

    public IReadOnlyCollection<GroupMember> Members => _members.AsReadOnly();

    private Group(string name) : base(Ulid.NewUlid())
    {
        Name = name;
    }

    public static Group Create(string name)
    {
        return new Group(name);
    }

    /// <summary>
    /// Adds new group member to group.
    /// </summary>
    /// <param name="userId">new group member UserId</param>
    /// <returns>True, when user was added as new group member otherwise False.</returns>
    public bool AddMember(Ulid userId)
    {
        if (!_members.Any(m => m.UserId == userId))
        {
            _members.Add(new GroupMember(userId, DateTime.UtcNow));
            return true;
        }

        return false;
    }

    public void RemoveMember(Ulid userId)
    {
        if (_members.FirstOrDefault(m => m.UserId == userId) is GroupMember member)
        {
            _members.Remove(member);
        }
    }

    private Group() : base(Ulid.NewUlid())
    { }
}