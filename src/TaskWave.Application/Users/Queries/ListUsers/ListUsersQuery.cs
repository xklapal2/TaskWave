using TaskWave.Application.Common.Security.Requests;
using TaskWave.Application.Common.Security.Permissions;
using TaskWave.Application.Common.Security.Policies;
using ErrorOr;
using TaskWave.Domain.Entities;
using MediatR;

namespace TaskWave.Application.Users.Queries.ListUsers;

[Authorize(Permissions = Permission.User.Get, Policies = Policy.SelfOrAdmin)]
public record ListUsersQuery() : IRequest<ErrorOr<List<User>>>;