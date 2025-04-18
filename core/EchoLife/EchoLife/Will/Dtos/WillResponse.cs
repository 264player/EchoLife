﻿using EchoLife.Will.Models;

namespace EchoLife.Will.Dtos;

public record WillResponse(string Id, string TestaorId, string ContentId)
{
    public static WillResponse From(OfficiousWill will)
    {
        return new WillResponse(will.Id, will.TestaorId, will.ContentId);
    }
}
