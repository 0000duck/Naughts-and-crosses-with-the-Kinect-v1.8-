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
    public partial class Trackbars : Form
    {
        CameraCapture CameraCapture;
        public int minhue=0;
        public int maxhue=255;
        public int minsat=0;
        public int maxsat=255;
        public int minval=0;
        public int maxval=255;
        public Trackbars(CameraCapture parent)
        {
            InitializeComponent();
            CameraCapture = parent;
        }

        private void Trackbars_Load(object sender, EventArgs e)
        {

        }

        private void huemin_Scroll(object sender, EventArgs e)
        {
            minhue = huemin.Value;
            
            huenum.Text = (minhue.ToString());

        }

        private void huemax_Scroll(object sender, EventArgs e)
        {
            maxhue = huemax.Value;
            huenum2.Text = (maxhue.ToString());
        }

        private void satmin_Scroll(object sender, EventArgs e)
        {
            minsat = satmin.Value;
            satnum.Text = (minsat.ToString());
        }

        private void satmax_Scroll(object sender, EventArgs e)
        {
            maxsat = satmax.Value;
            satnum2.Text = (maxsat.ToString());
        }

        private void valmin_Scroll(object sender, EventArgs e)
        {
            minval = valmin.Value;
            valnum.Text = (minval.ToString());
        }

        private void valmax_Scroll(object sender, EventArgs e)
        {
            maxval = valmax.Value;
            valnum2.Text = (maxval.ToString());
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Visible = false;
            //if board is being calibrated
            if (CameraCapture.boardcalib) CameraCapture.boardcalibed = true;
            
            //if mark is being calibrated
            if (CameraCapture.markcalib) CameraCapture.markcalibed = true;
           

        }
    }
}
