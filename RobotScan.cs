using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;

//Program for communicating with rapid controller
namespace CameraCapture
{
    public partial class RobotScan : Form
    {
        CameraCapture CameraCapture;

        private NetworkScanner scanner = new NetworkScanner();
        private Controller controller = null;
        public bool Targ = false;
        public RobotScan(CameraCapture parent)
        {
            InitializeComponent();
            CameraCapture = parent;
            
        }

        private void RobotScan_Load(object sender, EventArgs e)
        {
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            DisableControllerFunctionality();
            listView1.Items.Clear();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            ListViewItem item = null;
            foreach (ControllerInfo controllerInfo in controllers)
            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.Availability.ToString());
                item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                item.SubItems.Add(controllerInfo.SystemName);
                item.SubItems.Add(controllerInfo.Version.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                this.listView1.Items.Add(item);
                item.Tag = controllerInfo;
            }
        }

        private void EnableControllerFunctionality()
        {
            // put all the enable and disable functionality in the same place so that it is easy to reuse
            label1.Visible = false;
            listView1.Enabled = false;
            gbxControllerSelected.Visible = true;
        }
        private void DisableControllerFunctionality()
        {
            // put all the enable and disable functionality in the same place so that it is easy to reuse
            label1.Visible = true;
            listView1.Enabled = true;
            gbxControllerSelected.Visible = false;
            if (this.controller != null) //if selecting a new controller
            {
                this.controller.Logoff();
                this.controller.Dispose();
                this.controller = null;
            }
        }

        private void StartProduction()
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    ABB.Robotics.Controllers.RapidDomain.RapidData rd = controller.Rapid.GetRapidData("T_ROB1", "Module1", "Target_20");



                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {


                        //ABB.Robotics.Controllers.RapidDomain.RobTarget robtarget = new ABB.Robotics.Controllers.RapidDomain.RobTarget();
                        ABB.Robotics.Controllers.RapidDomain.Pos postarget = new ABB.Robotics.Controllers.RapidDomain.Pos();
                        postarget.X = CameraCapture.armtargetx;
                        postarget.Y = CameraCapture.armtargety;
                        postarget.Z = CameraCapture.rtz;

                        //robtarget.FillFromString2("[[x,0,0],[0.350268087,-0.408874117,0.842669585,-0.006495725],[1,-1,1,0],[9E9,9E9,9E9,9E9,9E9,9E9]]");
                        //robtarget.Trans.X = CameraCapture.rtx;
                        //robtarget.Trans.Y = CameraCapture.rty;
                        //robtarget.Trans.Z = CameraCapture.rtz;
                        //robtarget.Rot.Q1 = 0.350268087;
                        //robtarget.Rot.Q2 = -0.408874117;
                        //robtarget.Rot.Q3 = 0.842669585;
                        //robtarget.Rot.Q4 = -0.00649572;
                        //robtarget.Extax.Eax_a = 1;
                        //robtarget.Extax.Eax_b = -1;
                        //robtarget.Extax.Eax_c = 1;
                        //robtarget.Extax.Eax_d = 0;
                       
                        //rd.Value = robtarget;

                        this.controller.Rapid.Start();
                       
                    }

                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
            //set targetfound to false
            CameraCapture.targetfound = false;

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0]; if (item.Tag != null)
            {
                ControllerInfo controllerInfo = (ControllerInfo)item.Tag;
                if (controllerInfo.Availability == Availability.Available)
                {
                    if (controllerInfo.IsVirtual)
                    {
                        this.controller = ControllerFactory.CreateFrom(controllerInfo);
                        this.controller.Logon(UserInfo.DefaultUser);
                        listView1.Items.Clear();
                        listView1.Items.Add(item);
                        EnableControllerFunctionality();
                    }
                    else //real controller
                    {
                        if (MessageBox.Show("This is NOT a virtual controller, do you really want to connect to that?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.controller = ControllerFactory.CreateFrom(controllerInfo);
                            this.controller.Logon(UserInfo.DefaultUser);
                            listView1.Items.Clear();
                            listView1.Items.Add(item);
                            EnableControllerFunctionality();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selected controller not available.");
                }
            }

        }
        
        //Rapid controller button
        private void btnProduction_Click_1(object sender, EventArgs e)
        {

                if (controller.IsVirtual)
                {
                    
                    StartProduction();

                }
                else
                {
                    if (MessageBox.Show("Do you want to start production for the selected controller?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        StartProduction();

                    }
                }
            }
        }
    }

