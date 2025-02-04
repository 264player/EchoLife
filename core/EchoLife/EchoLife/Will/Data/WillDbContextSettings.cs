namespace EchoLife.Will.Data
{
    public class WillDbContextSettings
    {
        public string? MysqlConnetionString { get; set; } = null!;
        public string SqlLiteConnectionString { get; set; } = null!;
        public string WillTableName { get; set; } = null!;
        public string WillVersionTableName { get; set; } = null!;
    }
}
