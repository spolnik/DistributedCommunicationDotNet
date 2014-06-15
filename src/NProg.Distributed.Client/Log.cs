using System;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace NProg.Distributed.Client
{
    internal static class Log
    {
        private static readonly ILog Logger;

        static Log()
        {
            XmlConfigurator.Configure();
            Logger = LogManager.GetLogger(typeof(Program));
        }

        internal static void WriteLine(string format, params object[] args)
        {
            Task.Factory.StartNew(() => Logger.Debug(string.Format(format, args)));
        }

        public static void Error(Exception exception)
        {
            Logger.Error(exception.Message, exception);
        }
    }
}