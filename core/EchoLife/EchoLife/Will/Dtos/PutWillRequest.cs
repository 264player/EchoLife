using EchoLife.Will.Models;

namespace EchoLife.Will.Dtos;

public record PutWillRequest(string Name, WillType WillType, string VersionId);
