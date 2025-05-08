namespace EchoLife.Family.Dtos;

public record QueryFamilyHistoryRequest(string FamilyId, int Count = 30, string? CursorId = null);
