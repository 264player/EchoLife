using System.Text.Json.Serialization;

namespace EchoLife.Account.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccountRoles
{
    User,
    Reviewer,
    Admin,
}
