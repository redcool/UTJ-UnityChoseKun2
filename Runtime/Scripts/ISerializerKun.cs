namespace Utj.UnityChoseKun
{
    using System.IO;

    /// <summary>
    /// 用于Serialize/Deserialize的接口
    /// </summary>
    public interface ISerializerKun
    {

        // 规则
        // 成员为数组时，必须从元素数(4[byte])开始
        // 但是，null的情况下为-1
        // 成员为类时，考虑null的情况，与数组同样从-1or1开始
        

        /// <summary>
        /// 序列化Object
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        void Serialize(BinaryWriter binaryWriter);


        /// <summary>
        /// 反序列化Object
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        void Deserialize(BinaryReader binaryReader);
        

    }
}