namespace EchoLife.Life.Data
{
    public class LifeDbContextSettings
    {
        public string? MysqlConnetionString { get; set; } = null!;
        public string SqlLiteConnectionString { get; set; } = null!;
        public string LifeTableName { get; set; } = null!;
        public string LifePotintTableName { get; set; } = null!;
    }
}
