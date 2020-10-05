/*********************************************************************************************************************************************************************************************************************
 * Nom : Joey Martig
 * Date : 28.09.2020
 * Programme : EvaluationTIFF
 * Description : Lecture des données d'un fichier TIFF
 * Version: 1.0
 * CFPT-I
 *********************************************************************************************************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EvaluationTIFF
{
    public partial class FrmMain : Form
    {
        string filePath;
        FileStream fs;
        Dictionary<int, string> tagFormat;
        const string LITTLE_ENDIAN = "4949", BIG_ENDIAN = "4D4D";
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            tagFormat = new Dictionary<int, string>();
            tagFormat.Add(256, "ImageWidth");
            tagFormat.Add(257, "ImageLength");
            tagFormat.Add(258, "BitsPerSample");
            tagFormat.Add(259, "Compression");
            tagFormat.Add(262, "PhotometricInterpretation");
            tagFormat.Add(266, "FillOrder");
            tagFormat.Add(273, "StripOffsets");
            tagFormat.Add(274, "Orientation");
            tagFormat.Add(277, "SamplesPerPixel");
            tagFormat.Add(278, "RowsPerStrip");
            tagFormat.Add(279, "StripByteCounts");
            tagFormat.Add(284, "PlanarConfiguration");
            tagFormat.Add(296, "ResolutionUnit");
            tagFormat.Add(332, "PhotometricInterpretation");
            tagFormat.Add(338, "ExtraSamples");
            tagFormat.Add(339, "SampleFormat");
        }

        /// <summary>
        /// Ouvre une fenêtre permettant de sélectionner un fichier à traiter.
        /// </summary>
        private void BtnOpenfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "C:\\";
                ofd.Filter = "TIFF files (*.tiff)|*.tiff";

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

        /// <summary>
        /// Récupère les données du fichiers sélectionné et les affiches dans le textebox.
        /// </summary>
        private void GetData()
        {
            byte[] datas = new byte[8];
            int countEntries = 0;
            string byteOrder;

            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(datas, 0, datas.Length);
            byteOrder = string.Format("{0:X2}{1:X2}", datas[0], datas[1]);

            // Affiche la première entrée
            DisplayFirstEntry(countEntries);
            countEntries++;

            // Affichage de la deuxième entrée
            DisplaySecondEntry(countEntries, datas);
            countEntries++;

            // récupère l'offset de l'IFD
            datas = new byte[4]{ datas[4], datas[5], datas[6], datas[7]};
            fs.Seek(ConvertHexToDec(datas), SeekOrigin.Begin);
            datas = new byte[1000];
            fs.Read(datas, 0, datas.Length);

            // Parcours toutes les entrées et les affiches
            DisplayEntries(countEntries, datas, byteOrder);
        }

        /// <summary>
        /// Converti un tableau de byte contenant des valeurs hexadecimal en valeur décimal.
        /// </summary>
        /// <param name="hexToDec">tableau de byte contenant des valeurs hexacedimals</param>
        /// <returns>sommes du tableau converti en décimal</returns>
        private int ConvertHexToDec(byte[] hexToDec)
        {
            string toHex = "";
            for (int i = 0; i < hexToDec.Length; i++)
            {
                toHex += hexToDec[i].ToString("X2");
            }
            return Convert.ToInt32(toHex, 16);
        }
        private int ConvertHexToDec(byte hexToDec1, byte hexToDec2)
        {
            byte[] hexArray = { hexToDec1, hexToDec2 };
            return ConvertHexToDec(hexArray);
        }
        private int ConvertHexToDec(byte hexToDec1, byte hexToDec2, byte hexToDec3, byte hexToDec4)
        {
            byte[] hexArray = { hexToDec1, hexToDec2, hexToDec3, hexToDec4 };
            return ConvertHexToDec(hexArray);
        }

        private void DisplayFirstEntry(int pCountEntries)
        {
            tbxData.Text += string.Format("Entry number: {0}{1}", (pCountEntries + 1), Environment.NewLine);
            tbxData.Text += string.Format("FileName: {0}{1}", "Lenna.tiff", Environment.NewLine); // Je n'ai pas réussi à trouver où se situait le nom.
            tbxData.Text += Environment.NewLine;
        }

        private void DisplaySecondEntry(int pCountEntries, byte[] pDatas)
        {
            tbxData.Text += string.Format("Entry number: {0}{1}", (pCountEntries + 1), Environment.NewLine);
            tbxData.Text += string.Format("Byte order: {0:X2} {1:X2}{2}", pDatas[0], pDatas[1], Environment.NewLine);
            tbxData.Text += string.Format("Id: {0:X2} {1:X2}{2}", pDatas[2], pDatas[3], Environment.NewLine);
            tbxData.Text += string.Format("Offset: {0:X2} {1:X2} {2:X2} {3:X2}{4}", pDatas[4], pDatas[5], pDatas[6], pDatas[7], Environment.NewLine);
            tbxData.Text += Environment.NewLine;
        }

        /// <summary>
        /// Affiche toutes les données à l'exception des deux premières.
        /// </summary>
        private void DisplayEntries(int countEntries, byte[] datas, string byteOrder)
        {
            int index = 2;
            bool isNotDone = true;
            int numberOfDirectoryEntries = ConvertHexToDec(datas[0], datas[1]);
            while (isNotDone)
            {
                // défini la valeur du tag si elle existe dans le dictionnaire
                string tagvalue = "";
                if (tagFormat.ContainsKey(ConvertHexToDec(datas[index], datas[index + 1])))
                {
                    tagvalue = tagFormat[ConvertHexToDec(datas[index], datas[index + 1])];
                }

                tbxData.Text += string.Format("Entry number: {0}{1}", (countEntries + 1), Environment.NewLine);
                tbxData.Text += string.Format("tag: {0:X2} {1:X2} {2}{3}", datas[index], datas[index + 1], tagvalue, Environment.NewLine);
                tbxData.Text += string.Format("type: {0:X2} {1:X2}{2}", datas[index + 2], datas[index + 3], Environment.NewLine);
                tbxData.Text += string.Format("Count: {0:X2} {1:X2} {2:X2} {3:X2}{4}", datas[index + 4], datas[index + 5], datas[index + 6], datas[index + 7], Environment.NewLine);

                // affiche la valeur en fonction de l'ordre des bytes du fichier.
                switch (byteOrder)
                {
                    case LITTLE_ENDIAN:
                        tbxData.Text += string.Format("Value:{0}    Hexa: {1:X2} {2:X2} {3:X2} {4:X2}{5}    Decimal: {6}{7}",
                                        Environment.NewLine, datas[index + 8], datas[index + 9], datas[index + 10], datas[index + 11],
                                        Environment.NewLine, ConvertHexToDec(datas[index + 8], datas[index + 9], datas[index + 10], datas[index + 11]), Environment.NewLine);
                        break;
                    case BIG_ENDIAN:
                        tbxData.Text += string.Format("Value:{0}    Hexa: {1:X2} {2:X2} {3:X2} {4:X2}{5}    Decimal: {6}{7}",
                                        Environment.NewLine, datas[index + 10], datas[index + 11], datas[index + 8], datas[index + 9],
                                        Environment.NewLine, ConvertHexToDec(datas[index + 10], datas[index + 11], datas[index + 8], datas[index + 9]), Environment.NewLine);
                        break;
                    default:
                        tbxData.Text += "Value  " + "Error, can't define byte order" + Environment.NewLine;
                        break;
                }
                tbxData.Text += Environment.NewLine;

                index += 12;
                countEntries++;
                if (countEntries > numberOfDirectoryEntries)
                {
                    isNotDone = false;
                }
            }
        }
    }
}
