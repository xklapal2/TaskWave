using ErrorOr;

using MediatR;

namespace TaskWave.Application.Groups.Queries.GroupDetail;

public record GroupDetailQuery(Ulid Id) : IRequest<ErrorOr<GroupDetail>>;