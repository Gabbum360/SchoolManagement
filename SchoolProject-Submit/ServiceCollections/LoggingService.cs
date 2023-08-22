using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace SchoolProject_Submit.ServiceCollections
{
    public static class LoggingService
    {
        public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .Enrich.FromLogContext()
                .WriteTo.File(new CompactJsonFormatter(), @"Logs\log.json", retainedFileCountLimit: 10)
                .Filter.ByIncludingOnly(e => e.Level >= LogEventLevel.Error)
                .Filter.ByIncludingOnly(e => e.Level >= LogEventLevel.Information)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(ctx.Configuration));
            return builder;
        }
    }
}
