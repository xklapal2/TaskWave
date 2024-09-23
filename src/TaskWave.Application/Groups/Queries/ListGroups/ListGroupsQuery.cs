using ErrorOr;

using MediatR;

using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Groups.Queries.ListGroups;

public record ListGroupsQuery() : IRequest<ErrorOr<List<Group>>>;