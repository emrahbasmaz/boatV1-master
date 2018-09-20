using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Framework.UnitOfWork
{
    /// <summary>
    ///     Default UnitOfWork manages transactions and by default "DefaultFactoryName" named NHibernate Session Factory is used.
    /// N Level Nested UnitOfWork with the same factory nameor with Autonomous Transaction different factory name is supported.
    /// Nested UnitOfWorks: if uses same factory name then works with caller service connection.
    /// Limitations: Nested Different factory names is not suggested if you care about transactional integrity.
    /// To handle transactional integrity use with transaction scope and MSDTC must be enabled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        ///     appsettings.json ConnectionStrings section connection string name
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        ///     By default it's value is false, but if it's set to true then it will create new transaction to handle UnitOfWork
        /// </summary>
        public bool AutonomousTransaction { get; set; }

        /// <summary>
        ///     By default it's value is false, but if it's set to true by action filter commit operation will handled by UnitOfWorkActionFilter
        /// </summary>
        internal bool ManagedByActionFilter { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UnitOfWorkAttribute()
        {
            FactoryName = SessionProvider.DefaultFactoryName;
            AutonomousTransaction = false;
            ManagedByActionFilter = false;
        }
    }
}
