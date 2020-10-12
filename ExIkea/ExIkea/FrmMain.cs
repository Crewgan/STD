using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExIkea
{
    public partial class FrmMain : Form
    {
        Store store;
        Bitmap storeImage;
        Graphics g;
        public FrmMain()
        {
            InitializeComponent();
            store = new Store(new Size(500, 500), 50, 1, 30, 60, 5, 5);
            storeImage = new Bitmap(pcbStore.Width, pcbStore.Height);
        }

        private void Display_Tick(object sender, EventArgs e)
        {
            storeImage = new Bitmap(pcbStore.Width, pcbStore.Height);
            g = Graphics.FromImage(storeImage);
            store.Paint(g);

            pcbStore.Image = storeImage;
        }
    }
}
