﻿using EchoLife.Common.CRUD;

namespace EchoLife.Life.Models;

public class LifeSubSection : IEntity
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string LifeHistoryId { get; set; } = null!;
    public string? FatherId { get; set; }
    public int Index { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
