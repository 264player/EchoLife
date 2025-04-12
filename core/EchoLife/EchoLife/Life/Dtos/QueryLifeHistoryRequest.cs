namespace EchoLife.Life.Dtos;

public record QueryLifeHistoryRequest(int Count = 30, string? CursorId = null);
