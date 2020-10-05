using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex3
{
    public partial class FrmMain : Form
    {
        string filePath;
        FileStream fs;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnOpenfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "C:\\";
                ofd.Filter = "zip files (*.zip)|*.zip";

                if (fs != null)
                    fs.Close();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                    GetData();
                }
            }
        }

        private void GetData()
        {
            byte[] datas = new byte[1000];
            string[] datasName = {"Signature", "Version", "Flags", "Compression", "ModeTime", "ModeDate", "Crc-32", "CompressedSize", "UncompressedSize", "FileNameLen", "ExtraFieldLen", "FileName", "ExtraField"};
            int[] datasLength = { 4, 2, 2, 2, 2, 2, 4, 4, 4, 2, 2, 18, 16};
            List<string> data;

            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(datas, 0, datas.Length);
            tbxData.Text = "";

            for (int i = 0, j = 0; i < datasLength.Sum();i += datasLength[j], j++)
            {
                Console.WriteLine(datasName[j]);
                data = new List<string>();
                for (int x = 0; x < datasLength[j]; x++)
                {
                    Console.Write(datas[i+x].ToString("X") + " ");
                    data.Insert(0, datas[i + x].ToString("X"));
                }
                /*
                if (datasName[j] == "FileNameLen")
                {
                    // Ya encore du boulot
                    datasLength[11] = Convert.ToInt32(data);
                }*/
                
                Console.WriteLine();

                DataTreatment(datasName[j], data);
            }
            // Hex -> decimal -> binary
            Console.WriteLine(Convert.ToString( Convert.ToInt32("7d1c", 16) , 2));
        }

        private void DataTreatment(string dataName, List<string> hexData)
        {
            string value = "";
            string size = "";
            switch (dataName)
            {
                case "Signature":
                    foreach (var byteValue in hexData)
                    {
                        value = "\\x" + byteValue + value;
                    }
                    tbxData.Text += dataName + Environment.NewLine + value + Environment.NewLine;
                    break;
                case "Version":
                    string upperByte = hexData[0];
                    string lowerbyte = hexData[1];

                    tbxData.Text += dataName + Environment.NewLine + GetVersion(Convert.ToInt32(upperByte, 16), Convert.ToInt32(lowerbyte, 16).ToString()) + Environment.NewLine;
                    break;
                case "Flags":
                    string[] flags = { "encrypted file", "compression option", "compression option", "data descriptor", "enhanced deflation",
                                       "compressed patched data", "strong encryption", "unused", "unused", "unused", "unused", "language encoding",
                                       "reserved", "mask header values", "reserved", "reserved"};
                    foreach (var byteValue in hexData)
                    {
                        if (byteValue != "0")
                            value += byteValue;
                    }
                    if (value == "")
                        value = "0";

                    tbxData.Text += dataName + Environment.NewLine + flags[Convert.ToInt32(value, 16)] + Environment.NewLine;
                    break;
                case "Compression":
                    string[] compressionMethod = { "no compression", "shrunk", "reduced with compression factor 1", "reduced with compression factor 2", "reduced with compression factor 3",
                                       "reduced with compression factor 4", "imploded", "reserved", "deflated", "enhanced deflated", "PKWare DCL imploded", "reserved",
                                       "compressed using BZIP2", "reserved", "LZMA", "reserved", "compressed using IBM TERSE", "IBM LZ77 z"};
                    foreach (var byteValue in hexData)
                    {
                        if (byteValue != "0")
                            value += byteValue;
                    }
                    if (value == "")
                        value = "0";

                    tbxData.Text += dataName + Environment.NewLine + compressionMethod[Convert.ToInt32(value, 16)] + Environment.NewLine;
                    break;
                case "ModeTime":
                    break;
                case "ModeDate":
                    break;
                case "Crc-32":
                    foreach (var byteValue in hexData)
                    {
                        if (byteValue != "0")
                            value += byteValue;
                    }
                    tbxData.Text += dataName + Environment.NewLine + "0x" + value + Environment.NewLine;
                    break;
                case "CompressedSize":
                case "UncompressedSize":
                    foreach (var byteValue in hexData)
                    {
                        if (byteValue != "0")
                            size += byteValue;
                    }
                    tbxData.Text += dataName + Environment.NewLine + Convert.ToInt32(size, 16) + " bytes" + Environment.NewLine;
                    break;
                case "FileNameLen":
                    break;
                case "ExtraFieldLen":
                    break;
                case "FileName":
                    break;
                case "ExtraField":
                    break;
                default:
                    break;
            }
        }

        private string GetVersion(int upperByte, string lowerByte)
        {
            char[] result = lowerByte.ToCharArray();
            string[] systems = { "MS - DOS and OS / 2(FAT / VFAT / FAT32 file systems)",
                                 "Amiga", "OpenVMS", "UNIX", "VM/CMS", "Atari ST", "OS/2 H.P.F.S.",
                                 "Macintosh", "Z-System", "CP/M", "Windows NTFS", "MVS (OS/390 - Z/OS)",
                                 "VSE", "Acorn Risc", "VFAT", "alternate MVS", "BeOS", "Tandem", "OS/400",
                                 "OS/X (Darwin)", "255: unused"};
            

            return systems[upperByte] + " " + result[0] + "." + result[1];
        }
    }
}
