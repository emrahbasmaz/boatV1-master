using Boat.Framework.Interface;
using Boat.Framework.UnitOfWork;
using Castle.Core;
using Castle.Core.Logging;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Boat.Framework.Ioc
{
    /// <summary>
    /// Ioc installer
    /// </summary>
    public sealed class IocFactory
    {
        private IocFactory()
        {
            ILogger logger = IocFacility.Resolve<ILoggerFactory>().Create(typeof(IocFactory));

            logger.Info("IocFactory build started");
        }

        public IocFacility IocFacility
        {
            get
            {
                return IocFacility.Container;
            }
        }

        //internal class ServiceInstaller : IWindsorInstaller
        //{
        //    //public void Install(IWindsorContainer container, IConfigurationStore store)
        //    //{
        //    //    container.Register(Component.For<Shopper>().LifeStyle.Transient);
        //    //    container.Register(Component.For<ICreditCard>().ImplementedBy<MasterCard>().LifeStyle.Transient);
        //    //}

        //    public void Install(IWindsorContainer container, IConfigurationStore store)
        //    {
        //        container.Register(Component.For<Repository>().LifeStyle.Transient);
        //        container.Register(Component.For<IRepository>().ImplementedBy<Repository>().LifeStyle.Transient);
        //    }
        //}

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            //Do not intercept repositories.
            if (UnitOfWorkProvider.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                return;
            }

            //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkProvider.HasUnitOfWorkAttribute(method))
                {
                    //handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
                    return;
                }
            }
        }
    }
}
