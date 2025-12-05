using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine
{

    /// <summary>
    /// 用于序列化/反序列化Behaviour的Class
    /// Programed by Katsumasa.Kimura
    /// </summary>
    [System.Serializable]
    public class BehaviourKun : ComponentKun
    {
        /// <summary>
        /// 启用/禁用
        /// </summary>
        [SerializeField] bool m_enabled;
        
        
        public bool enabled{
            get {return m_enabled;}
            set {m_enabled = value;}
        }
        
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public BehaviourKun():this(null){}
        
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="component">Behaviour对象</param>
        public BehaviourKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Behaviour;
            var behaviour = component as Behaviour;
            if(behaviour != null){
                enabled = behaviour.enabled;
                name = behaviour.GetType().Name;
            }
        }


        /// <summary>
        /// 将内容写回到Behaviour
        /// </summary>
        /// <param name="component">要写回的Behaviour</param>
        /// <returns>结果 true:执行了写回</returns>
        public override bool WriteBack(Component component)
        {
            if(base.WriteBack(component))
            {
                var behaviour = component as Behaviour;
                if (behaviour)
                {
                    behaviour.enabled = enabled;
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_enabled);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_enabled = binaryReader.ReadBoolean();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as BehaviourKun;
            if(other == null)
            {
                return false;
            }
            if(enabled != other.enabled)
            {
                return false;
            }
            return base.Equals(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public static new BehaviourKun Allocater()
        {
            return new BehaviourKun();
        }


        public static new BehaviourKun[] Allocater(int len)
        {
            return new BehaviourKun[len];
        }

    }
}