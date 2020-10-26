/**********************************************************************************************************************************************************************************************************************
 * Name        : Joey Martig
 * Project     : exIkea
 * Date        : 26.10.2020
 * Description : Simulate a store with clients and checkouts.
 **********************************************************************************************************************************************************************************************************************/
using System;
using System.Drawing;
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
            store = new Store(new Size(500, 500), 50, 1, 30000, 60, 20, 5);
            storeImage = new Bitmap(pcbStore.Width, pcbStore.Height);
        }

        private void Display_Tick(object sender, EventArgs e)
        {
            storeImage = new Bitmap(pcbStore.Width, pcbStore.Height);
            g = Graphics.FromImage(storeImage);
            store.Actions(g);

            pcbStore.Image = storeImage;
        }
    }
}
