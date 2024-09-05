namespace TaskWave.Domain.Entities.Groups;

public class GroupMember
{
    public Ulid UserId { get; }
    public DateTime JoinedDate { get; }

    public GroupMember(Ulid userId, DateTime joinedDate)
    {
        UserId = userId;
        JoinedDate = joinedDate;
    }
}