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
        Store ikea;
        public FrmMain()
        {
            InitializeComponent();
            ikea = new Store(new Size(500, 500), 50, 3, 30, 60, 10, 5);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
