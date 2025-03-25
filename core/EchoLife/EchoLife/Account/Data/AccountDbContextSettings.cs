namespace EchoLife.Account.Data;

public class AccountDbContextSettings
{
    public string? MysqlConnectionString { get; set; }
    public string SqlLiteConnectionString { get; set; } = null!;
}
