using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mod_Tool_test
{
    public partial class BusyForm : Form
    {
        public Boolean isBusy = false;

        public BusyForm()
        {
            InitializeComponent();

            if(isBusy == true)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BusyForm_Load(object sender, EventArgs e)
        {

        }
    }
}
