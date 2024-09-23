using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Api.Contracts.Groups.Common;

public record GroupPreviewResponse(string Id, string Name);

public static class GroupPreviewResponseExtensions
{
    public static GroupPreviewResponse ToDto(this Group group)
        => new(
                group.Id.ToString(),
                group.Name
            );
}