using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex1
{
    public partial class FrmMain : Form
    {
        string filePath;
        Bitmap img;
        Color pixel;

        Dictionary<string, int> pixelFormat;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            pixelFormat = new Dictionary<string, int>();
            pixelFormat.Add("Format16bppArgb1555", 16);
            pixelFormat.Add("Format16bppGrayScale", 16);
            pixelFormat.Add("Format16bppRgb555", 16);
            pixelFormat.Add("Format16bppRgb565", 16);
            pixelFormat.Add("Format1bppIndexed", 1);
            pixelFormat.Add("Format24bppRgb", 24);
            pixelFormat.Add("Format32bppArgb", 32);
            pixelFormat.Add("Format32bppPArgb", 32);
            pixelFormat.Add("Format32bppRgb", 32);
            pixelFormat.Add("Format48bppRgb", 48);
            pixelFormat.Add("Format4bppIndexed", 4);
            pixelFormat.Add("Format64bppArgb", 64);
            pixelFormat.Add("Format64bppPArgb", 64);
            pixelFormat.Add("Format8bppIndexed", 8);

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()){
                ofd.InitialDirectory = "C:\\";
                ofd.Filter = "bmp files (*.bmp)|*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    img = new Bitmap(filePath);
                }
            }

            if (img != null)
            {
                // Temporaire:
                //lblSignature.Text = img.Height.ToString();
                //lblSizeBits.Text = GetSizeOfFile(img).ToString();
                lblWidth.Text = img.Width.ToString();
                lblHeight.Text = img.Height.ToString();
                lblBitByPixel.Text = img.PixelFormat.ToString();
                lblSizeBitsOfPicture.Text = GetSizeOfFile(img).ToString();

                //BinaryReader br = new BinaryReader();

                pcbImage.Image = new Bitmap(filePath);
                //pixel = img.GetPixel(0, 0);
            }
        }

        private int GetSizeOfFile(Bitmap img)
        {
            int result = 0;
            int bitByPixel = pixelFormat[img.PixelFormat.ToString()];
            
            result = Convert.ToInt32(img.Height * img.Width) * bitByPixel / 8;

            return result;
        }

        private void btnRevertImage_Click(object sender, EventArgs e)
        {
            int bitByPixel = pixelFormat[img.PixelFormat.ToString()];
            int arrayWidth = img.Width;
            int arrayHeight = img.Height;
            Color pixelColor;
            int pixelColorA;
            int pixelColorR;
            int pixelColorG;
            int pixelColorB;

            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    pixelColor = img.GetPixel(x, y);
                    pixelColorA = 255 - pixelColor.A;
                    pixelColorR = 255 - pixelColor.R;
                    pixelColorG = 255 - pixelColor.G;
                    pixelColorB = 255 - pixelColor.B;
                    img.SetPixel(x, y, Color.FromArgb(pixelColorA, pixelColorR, pixelColorG, pixelColorB));
                }
            }
            pcbImage.Image = img;
        }
    }
}
