namespace ChatAppServer.WebAPI.Dtos;

public sealed record SendMessageDto(
    Guid UserId,
    Guid ToUserId,
    string Message);
