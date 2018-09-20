using Boat.Framework.Ioc;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Framework
{
    public sealed class SessionProvider
    {
        #region Private Fields

        //private static readonly Lazy<SessionProvider> sessionProvider = new Lazy<SessionProvider>(() => new SessionProvider(DefaultFactoryName));

        private static readonly object _locker = new object();

        private readonly Dictionary<string, SessionProvider> sessionProviders = new Dictionary<string, SessionProvider>();
        private readonly ILogger logger = IocFacility.Container.Resolve<Castle.Core.Logging.ILoggerFactory>().Create(typeof(SessionProvider));

        #endregion

        #region Public Properties

        public static readonly string DefaultFactoryName = IocFacility.Container.AppSettings.GetValue<string>("DefaultFactoryName");

        public static IocFacility IocFacility
        {
            get
            {
                return IocFacility.Container;
            }
        }

        #endregion

    }
}
