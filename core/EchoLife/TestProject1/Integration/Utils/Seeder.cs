namespace EchoLife.Tests.Integration.Utils;

internal static class Seeder
{
    public static T Modify<T>(this Action<T>? action, T entity)
    {
        action?.Invoke(entity);
        return entity;
    }
}
