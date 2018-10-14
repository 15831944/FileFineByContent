using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace ContentFileFind.Helper
{
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    class CompressHelper
    {
        #region 压缩文件
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="destinationFile">目标文件</param>
        public static void CompressFile(string sourceFile, string destinationFile)
        {
            // 文件是否存在
            if (File.Exists(sourceFile) == false)
            {
                throw new FileNotFoundException();
            }

            byte[] buffer = null;
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream compressedStream = null;

            try
            {
                // 读取源文件到byte数组
                sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

                buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);

                if (checkCounter != buffer.Length)
                {
                    throw new ApplicationException();
                }

                destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);

                compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true);

                compressedStream.Write(buffer, 0, buffer.Length);
            }
            catch (ApplicationException ex)
            {
                throw (ex);
            }
            finally
            {
                if (sourceStream != null)
                    sourceStream.Close();

                if (compressedStream != null)
                    compressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }
        }
        #endregion

        #region 解压文件
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="destinationFile">目标文件</param>
        public static void DeCompressFile(string sourceFile, string destinationFile)
        {
            if (File.Exists(sourceFile) == false)
            {
                throw new FileNotFoundException();
            }

            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream decompressedStream = null;
            byte[] quartetBuffer = null;

            try
            {
                sourceStream = new FileStream(sourceFile, FileMode.Open);

                decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true);

                quartetBuffer = new byte[4];
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);

                byte[] buffer = new byte[checkLength + 100];

                int offset = 0;
                int total = 0;

                while (true)
                {
                    int bytesRead = decompressedStream.Read(buffer, offset, 100);

                    if (bytesRead == 0)
                        break;

                    offset += bytesRead;
                    total += bytesRead;
                }

                destinationStream = new FileStream(destinationFile, FileMode.Create);
                destinationStream.Write(buffer, 0, total);

                destinationStream.Flush();
            }
            catch (ApplicationException ex)
            {
                throw (ex);
            }
            finally
            {
                if (sourceStream != null)
                    sourceStream.Close();

                if (decompressedStream != null)
                    decompressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }

        }
        #endregion
    }
}
