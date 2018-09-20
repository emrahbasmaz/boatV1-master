using Boat.Framework.Interface;
using Boat.Framework.GenericRepository;
using Boat.Framework.Service;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace boatV1
{
    public class ServiceResolver : IServiceProvider
    {
        private static WindsorContainer container;
        private static IServiceProvider serviceProvider;

        public ServiceResolver(IServiceCollection services)
        {
            container = new WindsorContainer();
            //Register your components in container

            //then
            serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(container, services);
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
