using MediatR;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace TaskWave.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public sealed partial class AuthController(ISender mediator) : ApiController
{ }

[Route("users/")]
public sealed partial class UsersController(IMediator mediator) : ApiController
{ }

[Route("groups/")]
public sealed partial class GroupController(IMediator mediator) : ApiController
{ }