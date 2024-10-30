//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;

//namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration
//{
//    internal interface IScopedLoggerFactory: ILoggerFactory
//    {
//    }

//    internal class ScopedLoggerFactory : IScopedLoggerFactory
//    {
//        private readonly ILoggerFactory _loggerFactory;

//        public ScopedLoggerFactory(ILoggerFactory loggerFactory)
//        {
//            _loggerFactory = loggerFactory;
//        }

//        public void AddProvider(ILoggerProvider provider)
//        {
//            _loggerFactory.AddProvider(provider);
//        }

//        public ILogger CreateLogger(string categoryName)
//        {
//            var logger = _loggerFactory.CreateLogger(categoryName);
//            logger.BeginScope()
//        }

//        public void Dispose()
//        {
//            _loggerFactory.Dispose();
//        }
//    }

//    internal class ScopedLogger<T>(ILogger logger, T state) : ILogger
//    {
//        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
//        {
//            logger.Log(logLevel, eventId, state, exception, formatter);
//        }

//        public bool IsEnabled(LogLevel logLevel)
//        {
//            return logger.IsEnabled(logLevel);
//        }

//        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
//        {
//            return logger.BeginScope(state);
//        }
//    }
//}
