using ErrorOr;
using TaskWave.Domain.Entities;
using MediatR;

namespace TaskWave.Application.Users.Queries.ListUsers;

public record ListUsersQuery() : IRequest<ErrorOr<List<User>>>;