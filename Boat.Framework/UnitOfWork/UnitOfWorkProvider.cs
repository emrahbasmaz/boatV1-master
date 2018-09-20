using Boat.Framework.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Boat.Framework.UnitOfWork
{
    public class UnitOfWorkProvider 
    {
        private static AsyncLocal<UnitOfWorkProvider> current = new AsyncLocal<UnitOfWorkProvider>();

        private static AsyncLocal<Stack<UnitOfWorkProvider>> previous = new AsyncLocal<Stack<UnitOfWorkProvider>>();

        /// <summary>
        ///     Reference to the session factory.
        /// </summary>
        private readonly SessionProvider sessionProvider;

        /// <summary>
        ///     Referance working UnitOfWork attribute
        /// </summary>
        public UnitOfWorkAttribute UnitOfWork { get; private set; }

        /// <summary>
        ///     Reference to the currently running transcation.
        /// </summary>
       // private ITransaction transaction;

        /// <summary>
        ///     Creates a new instance of UnitOfWorkProvider.
        /// </summary>
        /// <param name="sessionProvider"></param>
        /// <param name="unitOfWork"></param>
        public UnitOfWorkProvider(SessionProvider sessionProvider, UnitOfWorkAttribute unitOfWork)
        {
            this.sessionProvider = sessionProvider;
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Gets current instance of the UnitOfWorkProvider.
        ///     It gets the right instance that is related to current thread. Owe to [ThreadStatic] attribute
        /// </summary>
        public static UnitOfWorkProvider Current
        {
            get { return current.Value; }
            set { current.Value = value; }
        }

        /// <summary>
        ///     Previous instance of the UnitOfWorkProviders for different factory names
        ///     It gets the right instance that is related to current thread. Owe to [ThreadStatic] attribute
        /// </summary>
        public static Stack<UnitOfWorkProvider> Previous
        {
            get
            {
                if (previous.Value == null)
                {
                    previous.Value = new Stack<UnitOfWorkProvider>();
                }

                return previous.Value;
            }
            set { previous.Value = value; }
        }


        internal static bool IsRepositoryMethod(MethodInfo methodInfo)
        {
            return IsRepositoryClass(methodInfo.DeclaringType);
        }

        internal static bool IsRepositoryClass(Type declaringType)
        {
            return typeof(IRepository).IsAssignableFrom(declaringType);
        }

        internal static bool HasUnitOfWorkAttribute(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }
}
