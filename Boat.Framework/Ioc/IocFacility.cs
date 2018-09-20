using Boat.Framework.Singleton;
using Microsoft.Extensions.Configuration;
using Castle.Windsor;
using Castle.Facilities.Logging;
using Castle.Services.Logging.Log4netIntegration;
using System.Collections.Generic;
using System.Reflection;

namespace Boat.Framework.Ioc
{
    /// <summary>
    /// Ioc Container Facility referance of WindsorContainer
    /// </summary>
    public sealed class IocFacility : WindsorContainer
    {
        public IConfigurationRoot AppSettings { get; set; }

        public Dictionary<string, Assembly> Assemblies { get; private set; } = new Dictionary<string, Assembly>();

        private IocFacility() : base()
        {
            AddFacility<LoggingFacility>(f => f.LogUsing<Log4netFactory>());
        }

        public static IocFacility Container
        {
            get
            {
                return Singleton<IocFacility>.Instance;
            }
        }
    }
}
