using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ContentFileFind.Helper
{
    /// <summary>
    /// XML序列化操作
    /// </summary>
    class XMLSerialize
    {
        #region Xml文件序列化
        /// <summary>
        /// 将Xml文件序列化(可起到加密和压缩XML文件的目的)
        /// </summary>
        /// <param name="FileFullPath">要序列化的XML文件全路径</param>
        /// <returns>bool 是否序列化成功</returns>
        public static bool SerializeXml(string FileFullPath)   //序列化：
        {
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet(); //创建数据集，用来临时存储XML文件
                DS.ReadXml(FileFullPath); //将XML文件读入到数据集中
                FileStream FS = new FileStream(FileFullPath + ".tmp", FileMode.OpenOrCreate); //创建一个.tmp的临时文件
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(); //使用二进制格式化程序进行序列化
                FT.Serialize(FS, DS); //把数据集序列化后存入文件中
                FS.Close(); //一定要关闭文件流，否则文件改名会报错（文件正在使用错误）
                IOHelper.DeleteFile(FileFullPath); //删除原XML文件
                File.Move(FileFullPath + ".tmp", FileFullPath); //改名(把临时文件名改成原来的xml文件名)
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 反序列化XML文件
        /// <summary>
        /// 反序列化XML文件
        /// </summary>
        /// <param name="FileFullPath">要反序列化XML文件的全路径</param>
        /// <returns>bool 是否反序列化XML文件</returns>
        public static bool DeSerializeXml(string FileFullPath)
        {
            FileStream FS = new FileStream(FileFullPath, FileMode.Open); //打开XML文件流
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(); //使用二进制格式化程序进行序列化
            ((System.Data.DataSet)FT.Deserialize(FS)).WriteXml(FileFullPath + ".tmp"); //把文件反序列化后存入.tmp临时文件中
            FS.Close(); //关闭并释放流
            IOHelper.DeleteFile(FileFullPath); //删除原文件
            File.Move(FileFullPath + ".tmp", FileFullPath); //改名(把临时文件改成原来的xml文件)
            return true;
        }
        #endregion
    }
}
