using System.IO;
using UnityEngine;
using UnityEngine.Rendering;


namespace Utj.UnityChoseKun
{
    /// <summary>
    /// 用于Serialize/Deserialize OnDemandRendering的类
    /// </summary>
    [System.Serializable]
    public class OnDemandRenderingKun : ISerializerKun
    {
        
        // 成员变量        
        [SerializeField] int mEffectiveRenderFrameRate;
        [SerializeField] int mRenderFrameInterval;
        [SerializeField] bool mWillCurrentFrameRender;
        [SerializeField] bool mIsDirty;


        // 属性

        public int effectiveRenderFrameRate
        {
            get { return mEffectiveRenderFrameRate; }
        }
        
        public int renderFrameInterval
        {
            get { return mRenderFrameInterval; }
            set { mRenderFrameInterval = value; }
        }
        
        
        public bool willCurrentFrameRender
        {
            get { return mWillCurrentFrameRender; }
        }


        public bool isDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }


        // 成员函数的定义

        /// <summary>
        /// 构造函数
        /// </summary>
        public OnDemandRenderingKun() : this(false) { }
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSet">true:设置值</param>
        public OnDemandRenderingKun(bool isSet):base()
        {
            if (isSet)
            {
#if UNITY_2019_3_OR_NEWER
                mEffectiveRenderFrameRate = OnDemandRendering.effectiveRenderFrameRate;
                mRenderFrameInterval = OnDemandRendering.renderFrameInterval;
                mWillCurrentFrameRender = OnDemandRendering.willCurrentFrameRender;
#endif
            }
        }


        /// <summary>
        /// 写回值
        /// </summary>
        public void WriteBack()
        {
            if (mIsDirty)
            {
#if UNITY_2019_3_OR_NEWER
                if (OnDemandRendering.renderFrameInterval != mRenderFrameInterval)
                {
                    OnDemandRendering.renderFrameInterval = mRenderFrameInterval;
                }
#endif
            }
        }


        /// <summary>
        /// 执行Serialize
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(mEffectiveRenderFrameRate);
            binaryWriter.Write(mRenderFrameInterval);
            binaryWriter.Write(mWillCurrentFrameRender);
            binaryWriter.Write(mIsDirty);
        }


        /// <summary>
        /// 执行Deserialize
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            mEffectiveRenderFrameRate = binaryReader.ReadInt32();
            mRenderFrameInterval = binaryReader.ReadInt32();
            mWillCurrentFrameRender = binaryReader.ReadBoolean();
            mIsDirty = binaryReader.ReadBoolean();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as OnDemandRenderingKun;
            if(other == null)
            {
                return false;
            }
            if (!int.Equals(mEffectiveRenderFrameRate, other.mEffectiveRenderFrameRate))
            {
                return false;
            }
            if (!int.Equals(mRenderFrameInterval, other.mRenderFrameInterval))
            {
                return false;
            }
            if (!bool.Equals(mWillCurrentFrameRender, other.mWillCurrentFrameRender))
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
            return base.GetHashCode();
        }
    }
}