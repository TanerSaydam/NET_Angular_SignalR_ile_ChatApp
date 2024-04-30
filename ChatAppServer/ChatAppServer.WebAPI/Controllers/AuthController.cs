using ChatAppServer.WebAPI.Context;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Models;
using GenericFileService.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController(
    ApplicationDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterDto request, CancellationToken cancellationToken)
    {
        bool isNameExists = await context.Users.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isNameExists)
        {
            return BadRequest(new { Message = "Bu kullanıcı adı daha önce kullanılmış" });
        }

        string avatar = FileService.FileSaveToServer(request.File, "wwwroot/avatar/");

        User user = new()
        {
            Name = request.Name,
            Avatar = avatar
        };

        await context.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> Login(string name, CancellationToken cancellationToken)
    {
        User? user = await context.Users.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

        if(user is null)
        {
            return BadRequest(new { Message = "Kullanıcı bulunamadı" });
        }

        user.Status = "online";

        await context.SaveChangesAsync(cancellationToken);

        return Ok(user);
    }
}
