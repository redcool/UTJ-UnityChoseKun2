using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utj.UnityChoseKun.Engine;

namespace PowerUtilities.UTJ
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class MonoBehaviourKunAttribute : Attribute
    {
        /// <summary>
        /// Component wrapper type
        /// </summary>
        public Type kunType;

    }
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class MonoBehaviourViewAttribute : Attribute
    {
        /// <summary>
        /// component wrapper type
        /// </summary>
        public Type kunType;
        public MonoBehaviourViewAttribute(Type kunType)
        {
            this.kunType = kunType;
        }
    }
    public class TestMAttribute : Attribute
    {
    }
}
