using EchoLife.Common;
using Microsoft.AspNetCore.Identity;

namespace EchoLife.Account.Models;

public class AccountRole : IdentityRole<string>
{
    public AccountRole(string roleName)
        : base(roleName)
    {
        Id = IdGenerator.GenerateUlid();
    }

    public AccountRole()
    {
        Id = IdGenerator.GenerateUlid();
    }
}
