namespace TaskWave.Application.Groups.Queries.GroupDetail;

public record GroupDetail(
    string Id,
    string Name,
    List<GroupMemberDetail> Members
);