namespace EchoLife.Common;

public static class IdGenerator
{
    public static string GenerateGuid(string model)
    {
        return Guid.NewGuid().ToString(model);
    }

    [Obsolete("To use sortable IDs, please use `GenerateUlid` to generate a ULID.")]
    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString();
    }

    public static string GenerateUlid()
    {
        return Ulid.NewUlid().ToString();
    }
}
