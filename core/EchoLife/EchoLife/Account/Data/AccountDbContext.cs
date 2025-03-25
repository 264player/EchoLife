using EchoLife.Account.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Account.Data;

public class AccountDbContext(DbContextOptions<AccountDbContext> options)
    : IdentityDbContext<IdentityAccount, AccountRole, string>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");
    }
}
