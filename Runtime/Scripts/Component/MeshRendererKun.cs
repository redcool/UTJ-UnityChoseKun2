using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine
{

    /// <summary>
    /// 用于Serialize/Deserialize MeshRenderer的类
    /// </summary>
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    public class MeshRendererKun : RendererKun
    {
        public MeshRendererKun():this(null){}

        public MeshRendererKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.MeshRenderer;
        }


        /// <summary>
        /// 写回到Component
        /// </summary>
        /// <param name="component">要写回的Component</param>
        /// <returns>结果 true : 执行了写回 false : 不需要写回</returns>
        public override bool WriteBack(Component component){
            if(base.WriteBack(component)){
                return true;
            }
            return false;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
        }
        
    }
}
