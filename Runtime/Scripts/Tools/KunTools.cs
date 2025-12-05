using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utj.UnityChoseKun.Engine;
using Object = UnityEngine.Object;

namespace PowerUtilities.UTJ
{
    public static class KunTools
    {
        /// <summary>
        /// {compKunType,kunViewType}
        /// </summary>
        public readonly static Dictionary<Type, Type> compKunViewTypeDict = new();

        /// <summary>
        /// Find all Type has MonoBehaviourViewAttribute and fill them in dict{kunType,kunViewType}
        /// </summary>
        /// <param name="dict"></param>
        public static void SetupComponentKunViewTypeDict(Dictionary<Type, Type> dict)
        {
            var viewTypes = ReflectionTools.GetTypesHasAttribute<MonoBehaviourViewAttribute>(true);

            foreach (var viewType in viewTypes)
            {
                var kunType = viewType.GetCustomAttribute<MonoBehaviourViewAttribute>()?.kunType;
                dict[kunType] = viewType;
            }
        }

        /// <summary>
        /// Get kun view viewType from kun viewType
        /// </summary>
        /// <param name="kunType"></param>
        /// <returns></returns>
        public static Type GetKunViewType(Type kunType)
        {
            if(compKunViewTypeDict.Count == 0)
                SetupComponentKunViewTypeDict(compKunViewTypeDict);

            if (compKunViewTypeDict.TryGetValue(kunType, out var kunViewType))
                return kunViewType;
            else
                return null;
        }

        static KunTools()
        {
            compKunViewTypeDict.Clear();
            SetupComponentKunViewTypeDict(compKunViewTypeDict);
        }

        /// <summary>
        /// Get Component wrapper viewType
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static Type GetKunType(Component comp)
        {
            return comp?.GetType().GetCustomAttribute<MonoBehaviourKunAttribute>(true)?.kunType;
        }

        /// <summary>
        /// Create instance for kun viewType full name
        /// </summary>
        /// <param name="kunTypeFullName"></param>
        /// <param name="kunObj"></param>
        /// <returns></returns>
        public static bool TryInstantiateKunType(string kunTypeFullName, out ComponentKun kunObj)
        {
            kunObj = null;

            var type = typeof(ComponentKun).Assembly.GetType(kunTypeFullName);
            if (type == null)
                type = ReflectionTools.GetTypeFromAppDomain(kunTypeFullName);
            if (type != null)
                kunObj = Activator.CreateInstance(type) as ComponentKun;
            return type != null;
        }

    }
}
