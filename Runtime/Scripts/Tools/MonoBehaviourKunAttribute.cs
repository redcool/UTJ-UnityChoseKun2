using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utj.UnityChoseKun.Engine;

namespace PowerUtilities.UTJ
{
    /// <summary>
    /// Mark this class wrapper class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class MonoBehaviourKunAttribute : Attribute
    {
        /// <summary>
        /// component wrapper type
        /// </summary>
        public Type kunType;
        public MonoBehaviourKunAttribute(Type kunType)
        {
            this.kunType = kunType;
        }
    }

}
