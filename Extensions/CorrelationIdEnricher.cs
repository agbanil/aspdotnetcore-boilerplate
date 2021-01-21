using System;
using Serilog.Core;
using Serilog.Events;

namespace WebApi.Extensions
{
    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private readonly string _propertyName;

        public CorrelationIdEnricher(string propertyName)
        {
            _propertyName = propertyName;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
        {
            string correlationId = System.Diagnostics.Activity.Current.RootId.ToString();
            logEvent.AddOrUpdateProperty(factory.CreateProperty(_propertyName, correlationId));
        }
    }
}