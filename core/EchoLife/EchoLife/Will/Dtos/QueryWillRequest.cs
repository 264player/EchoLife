namespace EchoLife.Will.Dtos;

public record QueryWillRequest(int Count, string? CursorId = null);
