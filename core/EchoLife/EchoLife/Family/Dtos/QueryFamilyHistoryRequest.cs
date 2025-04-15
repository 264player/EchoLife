namespace EchoLife.Family.Dtos;

public record QueryFamilyHistoryRequest(int Count = 30, string? CursorId = null);
