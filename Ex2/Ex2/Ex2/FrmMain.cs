using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Ex2
{
    public partial class FrmMain : Form
    {
        string filePath;
        FileStream fs;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "C:\\";
                ofd.Filter = "mp3 files (*.mp3)|*.mp3";

                if(fs != null)
                    fs.Close();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                    GetData();
                    BtnRead.Enabled = true;
                    BtnWrite.Enabled = true;
                }
            }
        }

        private void GetData()
        {
            byte[] datas = new byte[1000];
            Dictionary<int, char> byteToString = new Dictionary<int, char>();

            byteToString = InitializeASCII(byteToString);

            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(datas, 0, datas.Length);

            char previousChar = ' ';
            char separationChar = ';';
            string convertedDatas = "";
            foreach (byte data in datas)
            {
                if (byteToString.ContainsKey(data))
                {
                    //Console.Write(byteToString[data]);
                    previousChar = byteToString[data];
                    convertedDatas += previousChar;
                }
                else if (previousChar != separationChar)
                {
                    //Console.Write(separationChar);
                    previousChar = separationChar;
                    convertedDatas += previousChar;
                }  
            }
            string[] convertedDatasArray = convertedDatas.Split(';');
            string dataType = "";
            string newValue;

            tbxData.Text = convertedDatasArray[0] + Environment.NewLine;

            for (int i = 1; i < convertedDatasArray.Length; i++)
            {
                if (convertedDatasArray[i].Length >= 4)
                {
                    newValue = dataType;
                    dataType = convertedDatasArray[i].Substring(convertedDatasArray[i].Length - 4);
                    newValue += ": " + convertedDatasArray[i].Remove(convertedDatasArray[i].Length - 4);
                    tbxData.Text += newValue + Environment.NewLine;
                }
            }
            fs.Seek(0, SeekOrigin.Begin);

            Console.WriteLine(datas[7]);
            Console.WriteLine(datas[8]);
            Console.WriteLine(datas[9]);
            Console.WriteLine(datas[10]);

            ToBinary(datas[7], datas[8], datas[9], datas[10]);
            Console.WriteLine();
            Console.WriteLine(Encoding.Default.GetString(datas));
        }

        private int ToBinary(byte pByte)
        {

            return 0;
        }
        private int ToBinary(byte pByteOne, byte pByteTwo, byte pByteThree, byte pByteFour)
        {
            byte[] bytes = new byte[1] { pByteFour };
            List<int> toRemoveIndex = new List<int>();
            BitArray bits = new BitArray(bytes);
            string bitsValue = "";

            for (int i = bits.Length - 1; i >= 0; i--)
            {
                bitsValue += bits[i] ? '1' : '0';
            }

            for (int i = bits.Length - 1; i >= 0; i-=8)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (bits[i-j])
                    {
                        j = -1;
                    }
                    else
                    {
                        toRemoveIndex.Add(i - j);
                    }

                }
            }
            Console.WriteLine(bitsValue);
            bitsValue = "";
            // Ouga boulga
            for (int i = bits.Length-1, j = 0; i >= 0; i--)
            {
                Console.WriteLine(i +" "+ toRemoveIndex[j]);
                if (i != toRemoveIndex[j])
                {
                    
                    bitsValue += bits[i] ? '1' : '0';
                }
                else
                {
                    j++;
                }
            }

            Console.WriteLine(bitsValue);
            Console.WriteLine(Convert.ToInt32(bitsValue, 2));
            return Convert.ToInt32(bitsValue, 2);
        }

        private void SetData()
        {
            List<byte[]> byteArrays;
            List<TextBox> tbxArray;
            byte[] newTagId = new byte[3] {0,0,0};
            byte[] newTitle = new byte[30];
            byte[] newAuthor = new byte[30];
            byte[] newAlbum = new byte[30];
            byte[] newYear = new byte[4];
            byte[] newComment = new byte[30];
            byte[] newGenre = new byte[1];

            byteArrays = new List<byte[]> { newTagId, newTitle, newAuthor, newAlbum, newYear, newComment, newGenre };
            tbxArray = new List<TextBox> { tbxTag, tbxTitle, tbxAuthor, tbxAlbum, tbxYear, tbxComment, tbxGenre };

            for (int i = 0; i < byteArrays.Count; i++)
            {
                byteArrays[i] = Encoding.Default.GetBytes(tbxArray[i].Text);
            }

            //newTagId = Encoding.Default.GetBytes(tbxTag.Text);
            
            fs.Seek(-128, SeekOrigin.End);

            Console.WriteLine(byteArrays[1].Length);
            for (int i = 0; i < byteArrays.Count; i++)
            {
                fs.Write(byteArrays[i], 0, byteArrays[i].Length);
            }
        }

        private void BtnWrite_Click(object sender, EventArgs e)
        {
            SetData();
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private Dictionary<int, char> InitializeASCII(Dictionary<int, char> pByteToString)
        {
            for (int i = 48, value = 0; i < 58; i++, value++)
            {
                pByteToString.Add(i, Convert.ToChar(value.ToString()));
            }

            for (int i = 65; i < 90; i++)
            {
                pByteToString.Add(i, Convert.ToChar(i));
            }

            for (int i = 97; i < 123; i++)
            {
                pByteToString.Add(i, Convert.ToChar(i));
            }

            pByteToString.Add(32, ' ');
            pByteToString.Add(38, '&');
            pByteToString.Add(40, '(');
            pByteToString.Add(41, ')');
            pByteToString.Add(46, '.');
            pByteToString.Add(45, '-');

            return pByteToString;
        }
    }
}
