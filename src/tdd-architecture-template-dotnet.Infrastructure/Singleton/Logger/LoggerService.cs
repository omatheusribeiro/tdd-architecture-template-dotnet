﻿using Microsoft.Extensions.Logging;

namespace tdd_architecture_template_dotnet.Infrastructure.Singleton.Logger
{
    public class LoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation($"[INFO] {DateTime.UtcNow}: {message}");
        }

        public void LogError(string message)
        {
            _logger.LogError($"[ERROR] {DateTime.UtcNow}: {message}");
        }
    }

}
