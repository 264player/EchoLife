namespace EchoLife.Life.Data
{
    public class LifeDbContextSettings
    {
        public string? MysqlConnetionString { get; set; } = null!;
        public string SqlLiteConnectionString { get; set; } = null!;
        public string LifeUserPointMapTableName { get; set; } = null!;
        public string LifePointTableName { get; set; } = null!;
        public string LifeHistoryTableName { get; set; } = null!;
        public string LifeSubSectionTableName { get; set; } = null!;
        public string LifePointUriMapTableName { get; set; } = null!;
    }
}
