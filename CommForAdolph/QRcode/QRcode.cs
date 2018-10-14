using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Util;
using System.Web;
using Qr.Net.Imaging;

namespace CommForAdolph.QRcode
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public class QRcode
    {
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="size">图片大小</param>
        /// <param name="path">图片位置 
        /// 例如  /abc/abc/
        /// </param>
        /// <returns>返回生成的二维码图片路径</returns>
        public static string Create(string str, int size, string path)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                //设置编码模式
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //设置编码测量度
                qrCodeEncoder.QRCodeScale = size;
                //设置编码版本
                qrCodeEncoder.QRCodeVersion =4;
                //设置编码错误纠正
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                var encodedData = qrCodeEncoder.Encode(str);// QRcode.CreateQRCode(content, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 8, size, size, 0, codeEyeColor);
                var qrImage = new QrImage();
                Image image = qrImage.CreateImage(encodedData);

                //System.Drawing.Image image = qrCodeEncoder.Encode(str);

                string filename = "~" + path + Guid.NewGuid() + ".jpg";
                //HttpContext.Current.Request.MapPath(filename)
                image.Save(filename.Replace("~", ""));

                return filename.Replace("~", "");
            }
            catch (Exception)
            {
                return "";
            }

        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="size">二维码大小</param>
        /// <param name="targetPath">生成图片存放路径</param>
        /// <param name="codeEyeColor">颜色</param>
        /// <param name="border">边框宽度</param>
        /// <param name="backGroundPath">背景图路径</param>
        /// <param name="x">定位x</param>
        /// <param name="y">定位y</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns>返回生成的图片路径</returns>
        public static string CreateTGCode(string content, int size, string targetPath, System.Drawing.Color codeEyeColor, QrPosition position, int border = 0, string backGroundPath = "")
        {

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本
            qrCodeEncoder.QRCodeVersion = 8;
            //设置编码错误纠正
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            var encodedData = qrCodeEncoder.Encode(content);// QRcode.CreateQRCode(content, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 8, size, size, 0, codeEyeColor);
            var qrImage = new QrImage();
            Image img = qrImage.CreateImage(encodedData);
            string filename = "~" + targetPath + Guid.NewGuid() + ".jpg";

            if (string.IsNullOrEmpty(backGroundPath))
            {
                img.Save(HttpContext.Current.Request.MapPath(filename));
            }
            else
            {
                Image backimg = Image.FromFile(backGroundPath);//背景图片

                int x, y;
                if (position == QrPosition.Top)
                {
                    //y = backimg.Height / 2 - img.Height / 2
                }
                Image resultImg = QRcode.CombinImage(backimg, img, backimg.Width / 2 - img.Width / 2, backimg.Width / 2 - img.Width / 2, img.Width, img.Height);

                resultImg.Save(HttpContext.Current.Request.MapPath(filename));

            }

            return filename.Replace("~", "");
        }

        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static Image CombinImage(Image imgBack, string destImg)
        {
            Image img = Image.FromFile(destImg);        //照片图片      
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }

        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        /// <param name="x">定位x</param>
        /// <param name="y">定位y</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public static Image CombinImage(Image imgBack, Image destImg, int x, int y, int width, int height)
        {
            //Image img = Image.FromFile(destImg);        //照片图片      
            if (destImg.Height != 65 || destImg.Width != 65)
            {
                destImg = KiResizeImage(destImg, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(destImg, x, y, width, height);
            GC.Collect();
            return imgBack;
        }


        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="Content">内容文本</param>
        /// <param name="QRCodeEncodeMode">二维码编码方式</param>
        /// <param name="QRCodeErrorCorrect">纠错码等级</param>
        /// <param name="QRCodeVersion">二维码版本号 0-40</param>
        /// <param name="QRCodeScale">每个小方格的预设宽度（像素），正整数</param>
        /// <param name="size">图片尺寸（像素），0表示不设置</param>
        /// <param name="border">图片白边（像素），当size大于0时有效</param>
        /// <returns></returns>
        public static System.Drawing.Image CreateQRCode(string Content, QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode, QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect, int QRCodeVersion, int QRCodeScale, int size, int border)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncodeMode;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeErrorCorrect;
            qrCodeEncoder.QRCodeScale = QRCodeScale;
            qrCodeEncoder.QRCodeVersion = QRCodeVersion;

            var encodedData = qrCodeEncoder.Encode(Content);// QRcode.CreateQRCode(content, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 8, size, size, 0, codeEyeColor);
            var qrImage = new QrImage();
            Image image = qrImage.CreateImage(encodedData);

            //System.Drawing.Image image = qrCodeEncoder.Encode(Content);

            #region 根据设定的目标图片尺寸调整二维码QRCodeScale设置，并添加边框
            if (size > 0)
            {
                //当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                #region 当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                while (image.Width < size)
                {
                    qrCodeEncoder.QRCodeScale++;

                    Image imageNew = qrImage.CreateImage(encodedData);
                    //System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                    if (imageNew.Width < size)
                    {
                        image = new System.Drawing.Bitmap(imageNew);
                        imageNew.Dispose();
                        imageNew = null;
                    }
                    else
                    {
                        qrCodeEncoder.QRCodeScale--; //新尺寸未采用，恢复最终使用的尺寸
                        imageNew.Dispose();
                        imageNew = null;
                        break;
                    }
                }
                #endregion

                //当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                #region 当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                while (image.Width > size && qrCodeEncoder.QRCodeScale > 1)
                {
                    qrCodeEncoder.QRCodeScale--;
                    //var encodedData = qrCodeEncoder.Encode(Content);// QRcode.CreateQRCode(content, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 8, size, size, 0, codeEyeColor);
                    //var qrImage = new QrImage();
                    Image imageNew = qrImage.CreateImage(encodedData);

                    //System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                    image = new System.Drawing.Bitmap(imageNew);
                    imageNew.Dispose();
                    imageNew = null;
                    if (image.Width < size)
                    {
                        break;
                    }
                }
                #endregion

                //如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                #region 如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                if (image.Width <= size)
                {
                    //根据参数设置二维码图片白边的最小宽度
                    #region 根据参数设置二维码图片白边的最小宽度
                    if (border > 0)
                    {
                        while (image.Width <= size && size - image.Width < border * 2 && qrCodeEncoder.QRCodeScale > 1)
                        {
                            qrCodeEncoder.QRCodeScale--;
                            Image imageNew = qrImage.CreateImage(encodedData);
                            //System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                            image = new System.Drawing.Bitmap(imageNew);
                            imageNew.Dispose();
                            imageNew = null;
                        }
                    }
                    #endregion

                    //当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
                    if (image.Width < size)
                    {
                        //新建空白绘图
                        System.Drawing.Bitmap panel = new System.Drawing.Bitmap(size, size);
                        System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(panel);
                        int p_left = 0;
                        int p_top = 0;
                        if (image.Width <= size) //如果原图比目标形状宽
                        {
                            p_left = (size - image.Width) / 2;
                        }
                        if (image.Height <= size)
                        {
                            p_top = (size - image.Height) / 2;
                        }

                        //将生成的二维码图像粘贴至绘图的中心位置
                        graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
                        image = new System.Drawing.Bitmap(panel);
                        panel.Dispose();
                        panel = null;
                        graphic0.Dispose();
                        graphic0 = null;
                    }
                }
                #endregion
            }
            #endregion
            return image;
        }

        private static bool IsTrue() // 在Image类别对图片进行缩放的时候,需要一个返回bool类别的委托
        {
            return true;
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="Content">内容文本</param>
        /// <param name="QRCodeEncodeMode">二维码编码方式</param>
        /// <param name="QRCodeErrorCorrect">纠错码等级</param>
        /// <param name="QRCodeVersion">二维码版本号 0-40</param>
        /// <param name="QRCodeScale">每个小方格的预设宽度（像素），正整数</param>
        /// <param name="size">图片尺寸（像素），0表示不设置</param>
        /// <param name="border">图片白边（像素），当size大于0时有效</param>
        /// <returns></returns>
        public static System.Drawing.Image CreateQRCode(string Content, QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode, QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect, int QRCodeVersion, int QRCodeScale, int size, int border, System.Drawing.Color codeEyeColor)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncodeMode;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeErrorCorrect;
            qrCodeEncoder.QRCodeScale = QRCodeScale;
            qrCodeEncoder.QRCodeVersion = QRCodeVersion;
            var encodedData = qrCodeEncoder.Encode(Content);// QRcode.CreateQRCode(content, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 8, size, size, 0, codeEyeColor);
            var qrImage = new QrImage();
            Image image = qrImage.CreateImage(encodedData);

            //System.Drawing.Image image = qrCodeEncoder.Encode(Content);

            #region 根据设定的目标图片尺寸调整二维码QRCodeScale设置，并添加边框
            if (size > 0)
            {
                //当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                #region 当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                while (image.Width < size)
                {
                    qrCodeEncoder.QRCodeScale++;
                    Image imageNew = qrImage.CreateImage(encodedData);
                    //System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                    if (imageNew.Width < size)
                    {
                        image = new System.Drawing.Bitmap(imageNew);
                        imageNew.Dispose();
                        imageNew = null;
                    }
                    else
                    {
                        qrCodeEncoder.QRCodeScale--; //新尺寸未采用，恢复最终使用的尺寸
                        imageNew.Dispose();
                        imageNew = null;
                        break;
                    }
                }
                #endregion

                //当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                #region 当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                while (image.Width > size && qrCodeEncoder.QRCodeScale > 1)
                {
                    qrCodeEncoder.QRCodeScale--;
                    Image imageNew = qrImage.CreateImage(encodedData);
                    //System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                    image = new System.Drawing.Bitmap(imageNew);
                    imageNew.Dispose();
                    imageNew = null;
                    if (image.Width < size)
                    {
                        break;
                    }
                }
                #endregion

                //根据参数设置二维码图片白边的最小宽度（按需缩小）
                #region 根据参数设置二维码图片白边的最小宽度
                if (image.Width <= size && border > 0)
                {
                    while (image.Width <= size && size - image.Width < border * 2 && qrCodeEncoder.QRCodeScale > 1)
                    {
                        qrCodeEncoder.QRCodeScale--;
                        Image imageNew = qrImage.CreateImage(encodedData);
                        //System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                        image = new System.Drawing.Bitmap(imageNew);
                        imageNew.Dispose();
                        imageNew = null;
                    }
                }
                #endregion

                //已经确认二维码图像，为图像染色修饰
                if (true)
                {
                    //定位点方块边长
                    int beSize = qrCodeEncoder.QRCodeScale * 3;

                    int bep1_l = qrCodeEncoder.QRCodeScale * 2;
                    int bep1_t = qrCodeEncoder.QRCodeScale * 2;

                    int bep2_l = image.Width - qrCodeEncoder.QRCodeScale * 5 - 1;
                    int bep2_t = qrCodeEncoder.QRCodeScale * 2;

                    int bep3_l = qrCodeEncoder.QRCodeScale * 2;
                    int bep3_t = image.Height - qrCodeEncoder.QRCodeScale * 5 - 1;

                    int bep4_l = image.Width - qrCodeEncoder.QRCodeScale * 7 - 1;
                    int bep4_t = image.Height - qrCodeEncoder.QRCodeScale * 7 - 1;

                    System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(image);

                    // Create solid brush.
                    SolidBrush blueBrush = new SolidBrush(codeEyeColor == null ? System.Drawing.Color.Black : codeEyeColor);

                    // Fill rectangle to screen.
                    graphic0.FillRectangle(blueBrush, bep1_l, bep1_t, beSize, beSize);
                    graphic0.FillRectangle(blueBrush, bep2_l, bep2_t, beSize, beSize);
                    graphic0.FillRectangle(blueBrush, bep3_l, bep3_t, beSize, beSize);
                    graphic0.FillRectangle(blueBrush, bep4_l, bep4_t, qrCodeEncoder.QRCodeScale, qrCodeEncoder.QRCodeScale);
                }

                //当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
                #region 如果目标尺寸大于生成的图片尺寸，将二维码绘制在目标尺寸白色画布的中心位置
                if (image.Width < size)
                {
                    //新建空白绘图
                    System.Drawing.Bitmap panel = new System.Drawing.Bitmap(size, size);
                    System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(panel);
                    int p_left = 0;
                    int p_top = 0;
                    if (image.Width <= size) //如果原图比目标形状宽
                    {
                        p_left = (size - image.Width) / 2;
                    }
                    if (image.Height <= size)
                    {
                        p_top = (size - image.Height) / 2;
                    }

                    //将生成的二维码图像粘贴至绘图的中心位置
                    graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
                    image = new System.Drawing.Bitmap(panel);
                    panel.Dispose();
                    panel = null;
                    graphic0.Dispose();
                    graphic0 = null;
                }
                #endregion
            }
            #endregion
            return image;
        }
    }

    public enum QrPosition
    {
        Top = 1,
        Middle = 2,
        Bottom = 3
    }
}

/*
 * 引用ThoughtWorks.QRCode.dll
  QRcode qrCode = new QRcode();
    UserQrCode = qrCode.Create("http://www.kuiyu.net/" , 4, "/Content/Images/QrCode/");
 */