using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Boat.Framework.Singleton
{
    /// <summary>
    /// Generic Singleton Class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class
    {
        private Singleton() { }

        private static readonly Lazy<T> instance = new Lazy<T>(CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication);

        private static T CreateInstance()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }

        public static T Instance { get { return instance.Value; } }
    }
}
