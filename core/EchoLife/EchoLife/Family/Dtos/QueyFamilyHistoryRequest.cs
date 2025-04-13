namespace EchoLife.Family.Dtos;

public record QueyFamilyHistoryRequest(int Count = 30, string? CursorId = null);
