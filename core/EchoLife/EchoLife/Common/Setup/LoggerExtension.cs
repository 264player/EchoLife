namespace EchoLife.Common.Setup;

public static class LoggerExtension
{
    public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder logging)
    {
        logging.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.UseUtcTimestamp = true;
            options.SingleLine = true;
            options.TimestampFormat = "yy-MM-dd HH:mm:ss";
        });

        return logging;
    }
}
