using MediatR;

namespace TaskWave.Application.Common.Security.Requests;

public interface IAuthorizeableRequest<T> : IRequest<T>
{
    Ulid AuthorizedUserId { get; }
}