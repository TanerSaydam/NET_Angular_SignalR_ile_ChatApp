using ChatAppServer.WebAPI.Context;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Hubs;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ChatsController(
    ApplicationDbContext context,
    IHubContext<ChatHub> hubContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        List<User> users = await context.Users.OrderBy(p => p.Name).ToListAsync();
        return Ok(users);
    }

    [HttpGet]
    public async Task<IActionResult> GetChats(Guid userId, Guid toUserId, CancellationToken cancellationToken)
    {
        List<Chat> chats = 
            await context
            .Chats
            .Where(p => 
            p.UserId == userId && p.ToUserId == toUserId || 
            p.ToUserId == userId && p.UserId == toUserId)
            .OrderBy(p=> p.Date)
            .ToListAsync(cancellationToken);

        return Ok(chats);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageDto request, CancellationToken cancellationToken)
    {
        Chat chat = new()
        {
            UserId = request.UserId,
            ToUserId = request.ToUserId,
            Message = request.Message,
            Date = DateTime.Now
        };

        await context.AddAsync(chat, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        string connectionId = ChatHub.Users.First(p => p.Value == chat.ToUserId).Key;

        await hubContext.Clients.Client(connectionId).SendAsync("Messages", chat);

        return Ok(chat);
    }
}
