using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Groups.Common;

public record GroupResult(
    Ulid GroupId,
    string Name
)
{
    internal static GroupResult FromGroup(Group group) => new(
        group.Id,
        group.Name
    );
}