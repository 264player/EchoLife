namespace EchoLife.Will.Dtos;

public record QueryWillVersionRequest(int Count, string? CursorId = null);
