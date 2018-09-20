using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Boat.Framework.Logging
{
    public static class Log4NetExtension
    {
        public static Castle.Core.Logging.ILogger SetGlobalContext(this Castle.Core.Logging.ILogger logger, params object[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                log4net.GlobalContext.Properties[args[i].ToString()] = args[i + 1];
            }

            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo("log4net.config"));

            return logger;
        }

        public static Castle.Core.Logging.ILogger SetThreadContext(this Castle.Core.Logging.ILogger logger, params object[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                log4net.ThreadContext.Properties[args[i].ToString()] = args[i + 1];
            }

            return logger;
        }

        public static Castle.Core.Logging.ILogger SetLogicalThreadContext(this Castle.Core.Logging.ILogger logger, params object[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                log4net.LogicalThreadContext.Properties[args[i].ToString()] = args[i + 1];
            }

            return logger;
        }
    }
}
