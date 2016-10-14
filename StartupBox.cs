using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraCapture
{
    public partial class StartupBox : Form
    {
        CameraCapture CameraCapture;
        public StartupBox(CameraCapture parent)
        {
            InitializeComponent();
            CameraCapture = parent;
            
        }

        private void squaresbtn_Click(object sender, EventArgs e)
        {
            CameraCapture.P_mark = 2;
            CameraCapture.C_mark = 1;
            Visible = false;
            CameraCapture.start = false;
            MessageBox.Show("Ok I am naughts, You go First");

        }

        private void trianglesbtn_Click(object sender, EventArgs e)
        {
            CameraCapture.P_mark = 1;
            CameraCapture.C_mark = 2;
            Visible = false;
            CameraCapture.start = false;
            MessageBox.Show("Ok I am crosses, I go First");
        }
    }
}
