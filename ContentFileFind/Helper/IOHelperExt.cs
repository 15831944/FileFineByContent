using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace ContentFileFind
{
    public class IOHelperExt : IOHelper
    {
        #region 外部打开文件
        /// <summary>
        /// 根据传来的文件全路径，外部打开文件，默认用系统注册类型关联软件打开
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <returns>bool</returns>
        public static bool OpenFile(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                System.Diagnostics.Process.Start(FileFullPath); //打开文件，默认用系统注册类型关联软件打开
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 获取文件大小
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>string</returns>
        public static string GetFileSize(string FileFullPath)
        {
            if (Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath);
                long FL = F.Length;
                if (FL > (1024 * 1024 * 1024)) //由大向小来判断文件的大小
                {
                    return Math.Round((FL + 0.00) / (1024 * 1024 * 1024), 2).ToString() + " GB"; //将双精度浮点数舍入到指定的小数（long类型与double类型运算，结果会是一个double类型）
                }
                else if (FL > (1024 * 1024))
                {
                    return Math.Round((FL + 0.00) / (1024 * 1024), 2).ToString() + " MB";
                }
                else if (FL > 1024)
                {
                    return Math.Round((FL + 0.00) / 1024, 2).ToString() + " KB";
                }
                else
                {
                    return FL.ToString();
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 文件转换成二进制
        /// <summary>
        /// 文件转换成二进制，返回二进制数组Byte[]
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>byte[] 包含文件流的二进制数组</returns>
        public static byte[] FileToStreamByte(string FileFullPath)
        {
            if (Exists(FileFullPath))
            {
                FileStream FS = new FileStream(FileFullPath, FileMode.Open); //创建一个文件流
                byte[] fileData = new byte[FS.Length];                       //创建一个字节数组，用于保存流
                FS.Read(fileData, 0, fileData.Length);                       //从流中读取字节块，保存到缓存中
                FS.Close();                                                  //关闭流（一定得关闭，否则流一直存在）
                return fileData;                                             //返回字节数组
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 二进制生成文件
        /// <summary>
        /// 二进制数组Byte[]生成文件
        /// </summary>
        /// <param name="FileFullPath">要生成的文件全路径</param>
        /// <param name="StreamByte">要生成文件的二进制 Byte 数组</param>
        /// <returns>bool 是否生成成功</returns>
        public static bool ByteStreamToFile(string FileFullPath, byte[] StreamByte)
        {
            try
            {
                if (Exists(FileFullPath)) //判断要创建的文件是否存在，若存在则先删除
                {
                    File.Delete(FileFullPath);
                }
                FileStream FS = new FileStream(FileFullPath, FileMode.OpenOrCreate); //创建文件流(打开或创建的方式)
                FS.Write(StreamByte, 0, StreamByte.Length); //把文件流写到文件中
                FS.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 写日志文件
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="path">日志文件路径</param>
        /// <param name="msg">日志内容</param>
        public static void Log(string path, string msg)
        {
            DateTime dt = System.DateTime.Now;
            msg += "\r\n时间:" + dt.ToString() + " " + dt.Millisecond.ToString() + "------>";
            FileStream fs = new FileStream(path, FileMode.Append);
            try
            {
                //获得字节数组
                byte[] data = new System.Text.UTF8Encoding().GetBytes(msg);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
        #endregion

        #region 打开文件夹
        /// <summary>
        /// 浏览文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void ExplorePath(string path,bool isSelect=false)
        {
            if (isSelect)
                System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
            else
                System.Diagnostics.Process.Start("explorer.exe", path);
        }
        #endregion

        #region 获取路径下所有文件
        /// <summary>
        /// 获取路径下所有文件夹
        /// </summary>
        /// <param name="dirPath">路径</param>
        public static DirectoryInfo[] GetDirectoryInfo(DirectoryInfo dirInfo)
        {
            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
            return dirInfos;
        }

        /// <summary>
        /// 获取路径下所有文件
        /// </summary>
        /// <param name="dirPath">路径</param>
        /// <returns></returns>
        public static FileInfo[] GetFileInfos(DirectoryInfo dirInfo)
        {
            if (dirInfo != null)
            {
                return dirInfo.GetFiles();
            }
            else
                return null;
        }


        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="dirPath">文件路径</param>
        /// <param name="fileInfos">文件列表</param>
        /// <returns></returns>
        public static void GetAllFiles(DirectoryInfo dirInfo, ref List<FileInfo> fileInfos)
        {
            DirectoryInfo[] dirArry = GetDirectoryInfo(dirInfo);
            //获取文件列表
            FileInfo[] files = GetFileInfos(dirInfo);
            foreach (var item in files)
            {
                fileInfos.Add(item);
            }
            if (dirArry != null && dirArry.Length > 0)
            {
                foreach (var item in dirArry)
                {
                    GetAllFiles(item, ref fileInfos);
                }
            }
        }
        #endregion

        #region 按二进制形式读取查找文件
        /// <summary>
        /// 检查内容在文件中是否存在
        /// </summary>
        /// <param name="content">查找内容</param>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回-1时未找到</returns>
        public static int CheckContent(string content, string fileName)
        {
            byte[] byteContent = System.Text.Encoding.UTF8.GetBytes(content);

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] infbytes = new byte[(int)fs.Length];
            fs.Read(infbytes, 0, infbytes.Length);
            fs.Close();

            return IndexOf(infbytes, byteContent);
        }

        /// <summary>
        /// 报告指定的 System.Byte[] 在此实例中的第一个匹配项的索引。
        /// </summary>
        /// <param name="srcBytes">被执行查找的 System.Byte[]。</param>
        /// <param name="searchBytes">要查找的 System.Byte[]。</param>
        /// <returns>如果找到该字节数组，则为 searchBytes 的索引位置；如果未找到该字节数组，则为 -1。如果 searchBytes 为 null 或者长度为0，则返回值为 -1。</returns>
        public static int IndexOf(byte[] srcBytes, byte[] searchBytes, int offset = 0)
        {
            if (offset == -1) { return -1; }
            if (srcBytes == null) { return -1; }
            if (searchBytes == null) { return -1; }
            if (srcBytes.Length == 0) { return -1; }
            if (searchBytes.Length == 0) { return -1; }
            if (srcBytes.Length < searchBytes.Length) { return -1; }
            for (var i = offset; i < srcBytes.Length - searchBytes.Length; i++)
            {
                if (srcBytes[i] != searchBytes[0]) continue;
                if (searchBytes.Length == 1) { return i; }
                var flag = true;
                for (var j = 1; j < searchBytes.Length; j++)
                {
                    if (srcBytes[i + j] != searchBytes[j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) { return i; }
            }
            return -1;
        } 
        #endregion
    }
}