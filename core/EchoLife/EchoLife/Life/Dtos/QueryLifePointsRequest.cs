namespace EchoLife.Life.Dtos;

public record QueryLifePointsRequest(int Count = 30, string? CursorId = null);
