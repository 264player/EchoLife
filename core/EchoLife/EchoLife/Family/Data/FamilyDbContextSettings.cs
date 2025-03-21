namespace EchoLife.Family.Data;

public class FamilyDbContextSettings
{
    public string? MysqlConnetionString { get; set; } = null!;
    public string SqlLiteConnectionString { get; set; } = null!;
    public string FamilyTreeTableName { get; set; } = null!;
    public string FamilyMemberTableName { get; set; } = null!;
    public string FamilyHistoryTableName { get; set; } = null!;
    public string SubSectionTableName { get; set; } = null!;
}
