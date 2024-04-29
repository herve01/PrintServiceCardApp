using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintServiceCardApp
{
    public static class Functions
    {
        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        public static byte[] BitmapToByte(System.Drawing.Image bImage)
        {
            MemoryStream memory = null;
            try
            {
                memory = new MemoryStream();

                Bitmap img = new Bitmap(bImage);
                img.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);

            }
            catch (Exception)
            {
                ;
            }
            return memory.ToArray();
        }

        public static void InitTextBox(Control parent)
        {
            foreach (var control in parent.Controls.OfType<TextBox>())
            {
                if (control.Text != string.Empty)
                    control.Text = string.Empty;
            }
        }
        public static bool IsEmptyTextBox(Control parent)
        {
            foreach (var control in parent.Controls.OfType<TextBox>())
            {
                if (control.Text == string.Empty)
                    if(!control.Name.Contains("txtDescription"))
                        return true;
            }

            return false;
        }
    }
}
