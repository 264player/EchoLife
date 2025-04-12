namespace EchoLife.Life.Dtos;

public record QueryLifeSubSectionRequest(int Count = 30, string? CursorId = null);
