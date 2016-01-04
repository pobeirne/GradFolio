using System;
using GradFolio.Core.Services;
using NLog;
using NLog.Config;
using NLogWebExample.Services.Logging.NLog;

namespace GradFolio.Infrastructure.Logging.NLog
{
    public class LoggingService : ILoggingService
    {

        private readonly Logger _logger;

        public LoggingService()
        {
            ConfigurationItemFactory.Default.LayoutRenderers
              .RegisterDefinition("utc_date", typeof(UtcDateRenderer));
            ConfigurationItemFactory.Default.LayoutRenderers
                .RegisterDefinition("web_variables", typeof(WebVariablesRenderer));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.ErrorException(message: message, exception: x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }
    }
}
