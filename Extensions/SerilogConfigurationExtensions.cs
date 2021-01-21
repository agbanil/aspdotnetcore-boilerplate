using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;

namespace WebApi.Extensions
{
    public static class SerilogConfigurationExtensions
    {
        public static LoggerConfiguration WithCustomCorrelationId(this LoggerEnrichmentConfiguration configuration, string propertyName)
        {
            return configuration.With((ILogEventEnricher)new CorrelationIdEnricher(propertyName));
        }
    }
}