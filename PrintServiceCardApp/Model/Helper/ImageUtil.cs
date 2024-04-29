using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model.Helper
{
    public class ImageUtil
    {
        #region Images

        public static byte[] Bitmap2ToByte(Image image, System.Drawing.Size size)
        {
            MemoryStream memory = null;
            try
            {
                memory = new MemoryStream();

                Bitmap img = new Bitmap(image, size);
                img.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);

            }
            catch (Exception)
            {
                ;
            }
            return memory.ToArray();
        }

        public static Bitmap BitmapToBitmapImage(Image image)
        {
            MemoryStream memory = new MemoryStream();

            Bitmap img = new Bitmap(image);
            image.Save(memory, System.Drawing.Imaging.ImageFormat.Png);

            //memory.Position = 0;

            //img.BeginInit();
            //img.StreamSource = memory;

            //img.CacheOption = BitmapCacheOption.OnLoad;
            //img.EndInit();

            return img;
        }

        public static byte[] BitmapToByte(Image image)
        {
            MemoryStream memory = null;
            try
            {
                memory = new MemoryStream();

                Bitmap img = new Bitmap(image);
                img.Save(memory, System.Drawing.Imaging.ImageFormat.Png);

            }
            catch (Exception)
            {
                ;
            }
            return memory.ToArray();
        }

        public static Task<byte[]> BitmapToByteAsync(Bitmap bImage)
        {
            MemoryStream memory = null;
            byte[] array = null;
            try
            {
                memory = new MemoryStream();

                Bitmap img = new Bitmap(bImage);
                img.Save(memory, System.Drawing.Imaging.ImageFormat.Png);

                array = memory.ToArray();

            }
            catch (Exception)
            {
            }

            return Task.FromResult<byte[]>(array);
        }

        public static byte[] GetCodeQR(string text, int size = 30)
        {
            Zen.Barcode.BarcodeMetricsQr metricsQr = new Zen.Barcode.BarcodeMetricsQr();
            metricsQr.Version = 0;

            var codeQR = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var image = codeQR.Draw(text, metricsQr);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        //public static byte[] BitmapImageToByte(BitmapSource bImage)
        //{
        //    try
        //    {
        //        return BitmapToByte(BitmapImageToBitmap(bImage));
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public static byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        //{
        //    byte[] bytes = null;
        //    var bitmapSource = imageSource as BitmapSource;

        //    if (bitmapSource != null)
        //    {
        //        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

        //        using (var stream = new MemoryStream())
        //        {
        //            encoder.Save(stream);
        //            bytes = stream.ToArray();
        //        }
        //    }

        //    return bytes;
        //}

        public static Bitmap ByteToBitmap(byte[] bytes)
        {
            Bitmap img = null;
            MemoryStream memory = null;
            try
            {
                memory = new MemoryStream(bytes);

                img = new Bitmap(memory);
                img.Save(memory, System.Drawing.Imaging.ImageFormat.Png);

            }
            catch (Exception)
            {
            }

            return img;
        }

        //public static Bitmap BitmapImageToBitmap(BitmapSource bitmapImage)
        //{
        //    // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

        //    using (MemoryStream outStream = new MemoryStream())
        //    {
        //        BitmapEncoder enc = new BmpBitmapEncoder();
        //        enc.Frames.Add(BitmapFrame.Create(bitmapImage));
        //        enc.Save(outStream);
        //        System.Drawing.Bitmap bitmap = new Bitmap(outStream);

        //        return new Bitmap(bitmap);
        //    }
        //}

        public static byte[] ComputeThumbnail(byte[] image, int width, int height)
        {
            Bitmap bitmap = ByteToBitmap(image);

            int wB = bitmap.Width;
            int hB = bitmap.Height;
            float ratio = 0;

            int w = width, h = height;

            if (wB > hB)
            {
                ratio = (float)wB / hB;
                h = (int)((h / ratio) * ((float)w / h));
            }
            else
            {
                ratio = (float)hB / wB;
                w = (int)((w / ratio) * ((float)h / w));
            }

            return BitmapToByte(new Bitmap(bitmap.GetThumbnailImage(w, h, () => false, IntPtr.Zero)));
        }

        public static Bitmap ComputeThumbnailBitmap(byte[] image, int width, int height)
        {
            Bitmap bitmap = ByteToBitmap(image);

            int wB = bitmap.Width;
            int hB = bitmap.Height;
            float ratio = 0;

            int w = width, h = height;

            if (wB > hB)
            {
                ratio = (float)wB / hB;
                h = (int)((h / ratio) * ((float)w / h));
            }
            else
            {
                ratio = (float)hB / wB;
                w = (int)((w / ratio) * ((float)h / w));
            }

            w = w < wB ? w : wB;
            h = h < hB ? h : hB;

            return new Bitmap(bitmap.GetThumbnailImage(w, h, () => false, IntPtr.Zero));
        }

        public static Bitmap ComputeThumbnailBitmaps(byte[] image, int width, int height)
        {
            Bitmap bitmap = ByteToBitmap(image);

            int wB = bitmap.Width;
            int hB = bitmap.Height;
            float ratio = 0;

            int w = width, h = height;

            if (wB > hB)
            {
                ratio = (float)wB / hB;
                h = (int)((h / ratio) * ((float)w / h));
            }
            else
            {
                ratio = (float)hB / wB;
                w = (int)((w / ratio) * ((float)h / w));
            }

            w = w < wB ? w : wB;
            h = h < hB ? h : hB;

            return new Bitmap(bitmap.GetThumbnailImage(w, h, () => false, IntPtr.Zero));
        }

        public static byte[] ImageFileToByte(string file)
        {
            byte[] result = null;

            try
            {
                using (var memory = new MemoryStream())
                {
                    Bitmap img = new Bitmap(file);
                    img.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                    result = memory.ToArray();
                }
            }
            catch (Exception)
            {
            }

            return result;
        }

        #endregion
    }
}
