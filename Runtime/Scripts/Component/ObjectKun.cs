using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;


namespace Utj.UnityChoseKun{
    /// <summary>
    /// 用于在Runtime/Editor之间序列化/反序列化UnityEngine.Object的Class
    /// </summary>    
    [System.Serializable]    
    public class ObjectKun : ISerializerKun
    {
        static Dictionary<int,UnityEngine.Object> m_caches;


        public static void AddCache(UnityEngine.Object obj)
        {
            if(m_caches == null)
            {
                m_caches = new Dictionary<int, UnityEngine.Object>();
            }

            if (m_caches.ContainsKey(obj.GetInstanceID()) == false)
            {
                m_caches.Add(obj.GetInstanceID(), obj);
            }
        }


        public static UnityEngine.Object GetCache(int instanceID)
        {
            UnityEngine.Object result = null;
            if (m_caches != null)
            {
                m_caches.TryGetValue(instanceID, out result);
            }
            return result;
        }


        public static void ClearCache()
        {
            if (m_caches != null)
            {
                m_caches.Clear();
            }
        }



        [SerializeField] protected string m_name;
        [SerializeField] protected int m_instanceID;
        [SerializeField] bool m_dirty;


        public string name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        
        public int instanceID {
            get {return m_instanceID;}
             set {m_instanceID = value;}
        }


        public bool dirty {
            get {return m_dirty;}
            set {m_dirty = value;}
        }
        

        public virtual bool dirtyInHierarchy {
            get {return dirty;}
        }


        /// <summary>
        /// ObjectKun的构造函数
        /// </summary>
        public ObjectKun():this(null)
        {
            // TODO:int型InstanceID的获取方法确认
            byte[] gb = System.Guid.NewGuid().ToByteArray();
            instanceID = System.BitConverter.ToInt32(gb,0);            
        }


        /// <summary>
        /// ObjectKun的构造函数
        /// </summary>
        /// <param name="obj">Object类型的对象</param>
        public ObjectKun(UnityEngine.Object obj)
        {
            name = "";
            dirty = false;
            if (obj != null){
                name = obj.GetType().Name;
                instanceID = obj.GetInstanceID();    
            }
        }
        

        /// <summary>
        /// 将内容写回到Object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>结果 true:发生了写回 false:不需要写回</returns>
        public virtual bool WriteBack(UnityEngine.Object obj)
        {
            return dirty ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetInstanceID()
        {
            return instanceID;
        }


        /// <summary>
        /// 序列化Object
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_name);
            binaryWriter.Write(m_instanceID);
            binaryWriter.Write(m_dirty);
        }


        /// <summary>
        /// 反序列化Object
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {                        
            m_name = binaryReader.ReadString();
            m_instanceID = binaryReader.ReadInt32();
            m_dirty = binaryReader.ReadBoolean();
        }


        /// <summary>
        /// ObjectKun的比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var objectKun = obj as ObjectKun;
            if(objectKun == null)
            {
                return false;
            }
            if(string.Compare(name, objectKun.name) != 0)
            {
                return false;
            }            
            if (instanceID != objectKun.instanceID)
            {
                return false;
            }
            if(dirty != objectKun.dirty)
            {
                return false;
            }            
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return instanceID;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObjectKun Allocater()
        {
            return new ObjectKun();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static ObjectKun[] Allocater(int len)
        {
            return new ObjectKun[len];
        }
    }
}
