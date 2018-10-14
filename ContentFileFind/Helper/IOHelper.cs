using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
/*----------------------------------------------------------------
//文件名：IOHelper
//文件功能描述：文件操作类

----------------------------------------------------------------*/
namespace ContentFileFind
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class IOHelper
    {
        #region 判断文件是否存在
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool Exists(string fileName)
        {
            if (fileName == null || fileName.Trim() == "")
            {
                return false;
            }

            if (File.Exists(fileName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 目录是否存在
        /// </summary>
        /// <param name="dirPath">文件目录</param>
        /// <returns></returns>
        public static bool DirExists(string dirPath)
        {
            if (dirPath == null || dirPath.Trim() == "")
            {
                return false;
            }

            if (Directory.Exists(dirPath))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public static bool CreateDir(string dirName)
        {
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            return true;
        }
        #endregion

        #region 创建文件
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CreateFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
                fs.Dispose();
            }
            return true;

        }
        #endregion

        #region 文件读取
        /// <summary>
        /// 读文件内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Read(string fileName)
        {
            if (!Exists(fileName))
            {
                return null;
            }
            //将文件信息读入流中
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }

        /// <summary>
        /// 行读取
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadLine(string fileName)
        {
            if (!Exists(fileName))
            {
                return null;
            }
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return new StreamReader(fs).ReadLine();
            }
        }
        #endregion

        #region 写文件
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">文件内容</param>
        /// <returns></returns>
        public static bool Write(string fileName, string content)
        {
            if (!Exists(fileName) || content == null)
            {
                return false;
            }

            //将文件信息读入流中
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                lock (fs)//锁住流
                {
                    if (!fs.CanWrite)
                    {
                        throw new System.Security.SecurityException("文件fileName=" + fileName + "是只读文件不能写入!");
                    }

                    byte[] buffer = Encoding.Default.GetBytes(content);
                    fs.Write(buffer, 0, buffer.Length);
                    return true;
                }
            }
        }

        /// <summary>
        /// 写入一行
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool WriteLine(string fileName, string content)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate | FileMode.Append))
            {
                lock (fs)
                {
                    if (!fs.CanWrite)
                    {
                        throw new System.Security.SecurityException("文件fileName=" + fileName + "是只读文件不能写入!");
                    }

                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(content);
                    sw.Dispose();
                    sw.Close();
                    return true;
                }
            }
        }
        #endregion

        #region 追加文本
        /// <summary>
        /// 追加文本
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strings">内容</param>
        public static void FileAdd(string FileFullPath, string strings)
        {
            StreamWriter sw = File.AppendText(FileFullPath);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
        }
        #endregion

        #region 目录复制
        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="fromDir">被复制的目录</param>
        /// <param name="toDir">复制到的目录</param>
        /// <returns></returns>
        public static bool CopyDir(string fromDir, string toDir)
        {
            if (fromDir == null || toDir == null)
            {
                throw new NullReferenceException("参数为空");
            }

            if (fromDir == toDir)
            {
                throw new Exception("两个目录都是" + fromDir);
            }

            if (!Directory.Exists(fromDir))
            {
                throw new IOException("目录fromDir=" + fromDir + "不存在");
            }

            DirectoryInfo dir = new DirectoryInfo(fromDir);
            return CopyDir(dir, toDir, dir.FullName);
        }


        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="fromDir">被复制的目录</param>
        /// <param name="toDir">复制到的目录</param>
        /// <param name="rootDir">被复制的根目录</param>
        /// <returns></returns>
        private static bool CopyDir(DirectoryInfo fromDir, string toDir, string rootDir)
        {
            string filePath = string.Empty;
            foreach (FileInfo f in fromDir.GetFiles())
            {
                filePath = toDir + f.FullName.Substring(rootDir.Length);
                string newDir = filePath.Substring(0, filePath.LastIndexOf("\\"));
                CreateDir(newDir);
                File.Copy(f.FullName, filePath, true);
            }

            foreach (DirectoryInfo dir in fromDir.GetDirectories())
            {
                CopyDir(dir, toDir, rootDir);
            }

            return true;
        }

        /// <summary>
        /// 目录复制
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <returns></returns>
        public static bool CopyDir(DirectoryInfo fromDir, string toDir)
        {
            return CopyDir(fromDir, toDir, fromDir.FullName);
        }
        #endregion

        #region 文件拷贝(可覆盖）
        /// <summary>
        /// 文件拷贝(可覆盖）
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="destFile">目标文件</param>
        /// <returns></returns>
        public static bool FileCopy(string sourceFile, string destFile)
        {
            bool ret = false;

            if (File.Exists(sourceFile))
            {
                try
                {
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Copy(sourceFile, destFile);
                    ret = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            }

            return ret;
        }
        #endregion

        #region 删除文件

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileFullPath">文件的全路径.</param>
        /// <returns>bool</returns>
        public static bool DeleteFile(string FileFullPath)
        {
            if (Exists(FileFullPath)) //用静态类判断文件是否存在
            {
                File.SetAttributes(FileFullPath, FileAttributes.Normal); //设置文件的属性为正常（如果文件为只读的话直接删除会报错）
                File.Delete(FileFullPath); //删除文件
                return true;
            }
            else //文件不存在
            {
                return false;
            }
        }
        #endregion

        #region 目录删除
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">制定目录</param>
        /// <param name="onlyDir">是否只删除目录</param>
        /// <returns></returns>
        public static bool DeleteDir(string dir, bool onlyDir)
        {
            if (dir == null || dir.Trim() == "")
            {
                throw new NullReferenceException("目录dir=" + dir + "不存在");
            }

            if (!Directory.Exists(dir))
            {
                return false;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            if (dirInfo.GetFiles().Length == 0 && dirInfo.GetDirectories().Length == 0)
            {
                Directory.Delete(dir);
                return true;
            }


            if (!onlyDir)
            {
                return false;
            }
            else
            {
                DeleteDir(dirInfo);
                return true;
            }
        }
        /// <summary>
        /// 目录删除
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteDir(DirectoryInfo dir)
        {
            if (dir == null)
            {
                throw new NullReferenceException("目录不存在");
            }

            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                DeleteDir(d);
            }

            foreach (FileInfo f in dir.GetFiles())
            {
                DeleteFile(f.FullName);
            }

            dir.Delete();

        }
        #endregion

        #region 文件查找
        /// <summary>
        /// 在指定的目录中查找文件
        /// </summary>
        /// <param name="dir">目录字符串</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool FindFile(string dir, string fileName)
        {
            if (dir == null || dir.Trim() == "" || fileName == null || fileName.Trim() == "" || !Directory.Exists(dir))
            {
                return false;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return FindFile(dirInfo, fileName);

        }

        /// <summary>
        /// 文件查找
        /// </summary>
        /// <param name="dir">路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool FindFile(DirectoryInfo dir, string fileName)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (File.Exists(d.FullName + "\\" + fileName))
                {
                    return true;
                }
                FindFile(d, fileName);
            }

            return false;
        }
        #endregion

        #region 获取文件名（包含扩展名）
        /// <summary>
        /// 获取文件名（包含扩展名）
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>string</returns>
        public static string GetFileName(string FileFullPath)
        {
            if (Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath); //FileInfo类为提供创建、复制、删除等方法
                return F.Name; //获取文件名（包含扩展名）
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取文件扩展名
        /// <summary>
        /// 获取文件文件扩展名
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>string</returns>
        public static string GetFileExtension(string FileFullPath)
        {
            if (Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath);
                return F.Extension; //获取文件扩展名（包含".",如：".mp3"）
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取文件名（可包含扩展名）
        /// <summary>
        /// 获取文件名（可包含扩展名）
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <param name="IncludeExtension">是否包含扩展名</param>
        /// <returns>string</returns>
        public static string GetFileName(string FileFullPath, bool IncludeExtension)
        {
            if (Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath);
                if (IncludeExtension)
                {
                    return F.Name;   //返回文件名（包含扩展名）
                }
                else
                {
                    return F.Name.Replace(F.Extension, ""); //把扩展名替换为空字符
                }
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}