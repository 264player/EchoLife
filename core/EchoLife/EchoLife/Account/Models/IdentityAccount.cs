using Microsoft.AspNetCore.Identity;

namespace EchoLife.Account.Models;

public class IdentityAccount : IdentityUser<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString("N");
}
