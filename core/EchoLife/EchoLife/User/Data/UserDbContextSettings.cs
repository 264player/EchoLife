namespace EchoLife.User.Data
{
    public class UserDbContextSettings
    {
        public string? MysqlConnetionString { get; set; } = null!;
        public string SqlLiteConnectionString { get; set; } = null!;
        public string BaseUserTableName { get; set; } = null!;
    }
}
