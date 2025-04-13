namespace EchoLife.Family.Dtos;

public record QueryFamilySubSectionRequest(int Count = 30, string? CursorId = null);
