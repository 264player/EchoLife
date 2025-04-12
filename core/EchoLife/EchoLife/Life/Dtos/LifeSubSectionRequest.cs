namespace EchoLife.Life.Dtos;

public record LifeSubSectionRequest(
    string Title,
    string Content,
    string LifeHistoryId,
    string? FatherId,
    int Deep
);
