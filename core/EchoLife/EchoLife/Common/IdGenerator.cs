namespace EchoLife.Common;

public static class IdGenerator
{
    public static string GenerateGuid(string model)
    {
        return Guid.NewGuid().ToString(model);
    }

    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString();
    }

    public static string GenerateUlid()
    {
        return Ulid.NewUlid().ToString();
    }
}
