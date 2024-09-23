namespace TaskWave.Api.Contracts.Groups;

public record AddGroupMembersRequest(
    Ulid GroupId,
    HashSet<Ulid> UserIds
);