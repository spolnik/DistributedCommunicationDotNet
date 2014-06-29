using System;
using log4net;
using log4net.Config;

namespace NProg.Distributed.Transport.Tests
{
    internal static class Log
    {
        private static readonly ILog Logger;

        static Log()
        {
            XmlConfigurator.Configure();
            Logger = LogManager.GetLogger(typeof(Log));
        }

        public static void WriteLine(string format, params object[] args)
        {
            Logger.Debug(string.Format(format, args));
        }

        public static void Error(Exception exception)
        {
            Logger.Error(exception.Message, exception);
        }
    }
}