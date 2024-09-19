using Microsoft.AspNetCore.Mvc;

namespace TaskWave.Api.Controllers;

[Route("groups/")]
public class GroupController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateGroup()
    {
        return Ok();
    }
}