using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.Util;
using System.IO;

using System.Windows.Media;
using System.Windows.Media.Imaging;

using Emgu.CV.Cuda;
using Emgu.CV.UI;

using ABB.Robotics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;


using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

using System.Diagnostics;
using System.Windows;
using System.Globalization;



namespace CameraCapture
{
    public partial class CameraCapture : Form
    {
        RobotScan RobotScan;
        StartupBox StartupBox;
        Trackbars Trackbars;
        
        //declaring global variables
        private Capture capture = null;        //takes images from camera as image frames
        private bool captureInProgress; // checks if capture is executing

        //board positions (found from boardcalibration)
        int BOARD_SIZE = new int();
        public int boardxorigin = new int();
        public int boardyorigin = new int();

        //create gameboard
        gameboard[,] gboard = new gameboard[3,3]{ { new gameboard(), new gameboard(), new gameboard() }, { new gameboard(), new gameboard(), new gameboard() }, { new gameboard(), new gameboard(), new gameboard() } };

              
        public int[,] board = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        public int armtargetx;
        public static int armtargety;
        public static int rtz = 104;

        public int P_mark;
        public int C_mark;
        public double angle;

        //Turn Number
        int turnNum = 0;

        //public bools
        //calibration bools
        public bool markcalib = false;
        public bool markcalibed = false;
        public bool showmarks=false;

        public bool boardcalib = false;
        public bool boardcalibed = false;
        public bool showboard = false;

        //tracking and finding board bools
        public bool tracking = false;
        public bool boardfind = false;
        public bool boardfound = false;
        public bool targetfound = false;

        //lockin move button
        public bool Lockin = false;
        public bool boardchanged=false;

        //
        public bool gameinprogress = false;
        public bool start = false;
        bool turn = false;
        public bool startbox = true;

        //board calib hsv values
        public int bhmin = 0;
        public int bhmax = 255;
        public int bsmin = 0;
        public int bsmax = 255;
        public int bvmin = 0;
        public int bvmax = 255;

        //Mark calib hsv values
        public int mhmin = 0;
        public int mhmax = 255;
        public int msmin = 0;
        public int msmax = 255;
        public int mvmin = 0;
        public int mvmax = 255;

        //kinect variables
        KinectSensor sensor;
        WriteableBitmap depthBitmap;
        WriteableBitmap colorBitmap;
        DepthImagePixel[] depthPixels;
        private ColorImagePoint[] colorCoordinates;
        byte[] colorPixels;


        public CameraCapture()
        {

            InitializeComponent();
            Trackbars = new Trackbars(this);
            Trackbars.Visible = false;
            StartupBox = new StartupBox(this);
            StartupBox.Visible = false;
           
          

            RobotScan = new RobotScan(this);
            RobotScan.Visible = true;

            //initiate kinect camera
            kinectstart();

        }
        

        //Game Logic
        void strategy()
        {
            //win variable
            bool win = false;

            //looking for move variable
            bool movefound = false;

            while (!movefound)
                for (int i = 0; i <= 2; i++)
                {
                    //check horizontal
                    if ((board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) && board[i, 2]!=0)
                    {
                        if (board[i, 0] == P_mark) Console.WriteLine("congratulations you win!");
                        if (board[i, 0] != P_mark) Console.WriteLine("I win!");
                    }

                    //check verticals
                    if ((board[0, i] == board[1, i]) && board[1, i] == board[2, i] && board[2, i] != 0)
                    {
                        if (board[0, i] == P_mark) Console.WriteLine("congratulations you win!");
                        if (board[0, i] != P_mark) Console.WriteLine("I win!");
                    }


                    //check diagonals
                    if ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[1, 1] != 0) || (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) && board[1,1] != 0)
                    {
                        if (board[1, 1] == P_mark) Console.WriteLine("congratulations you win!");
                        if (board[1, 1] != P_mark) Console.WriteLine("I win!");
                    }

                    //Check for winning moves, 1st check horizontal lines, then vertical lines then diagonals

                    //for j=1 check for winning moves, for j=2 stop opponent winning moves
                    for (int j = 1; j <= 2; j++)
                    {
                        int z = 0;
                        if (j == 1) z = P_mark;
                        if (j == 2) z = C_mark;
                        //check horizontal
                        if (board[i, 0] == board[i, 1] && board[i, 2] == 0 && board[i, 0] != z)
                        {
                            armtargetx = gboard[i, 2].getxpos(); armtargety = gboard[i, 2].getypos();
                            movefound = true;
                        }
                        if (board[i, 1] == board[i, 2] && board[i, 0] == 0 && board[i, 1] != z)
                        {
                            armtargetx = gboard[i, 0].getxpos(); armtargety = gboard[i, 0].getypos();
                            movefound = true;
                        }
                        if (board[i, 0] == board[i, 2] && board[i, 1] == 0 && board[i, 0] != z)
                        {
                            armtargetx = gboard[i, 1].getxpos(); armtargety = gboard[i, 1].getypos();
                            movefound = true;
                        }

                        //check vertical
                        if (board[0, i] == board[1, i] && board[2, i] == 0 && board[i, 0] != z)
                        {
                            armtargetx = gboard[2, i].getxpos(); armtargety = gboard[2, i].getypos();
                            movefound = true;
                        }
                        if (board[1, i] == board[2, i] && board[0, i] == 0 && board[1, i] != z)
                        {
                            armtargetx = gboard[0, i].getxpos(); armtargety = gboard[0, i].getypos();
                            movefound = true;
                        }
                        if (board[0, i] == board[2, i] && board[1, i] == 0 && board[0, i] != z)
                        {
                            armtargetx = gboard[1, i].getxpos(); armtargety = gboard[1, i].getypos();
                            movefound = true;
                        }

                        //check Diagonal (top left-Bottom Right)
                        if (board[0, 0] == board[1, 1] && board[2, 2] == 0 && board[0, 0] != z)
                        {
                            armtargetx = gboard[2, 2].getxpos(); armtargety = gboard[2, 2].getypos();
                            movefound = true;
                        }
                        if (board[0, 0] == board[2, 2] && board[1, 1] == 0 && board[0, 0] != z)
                        {
                            armtargetx = gboard[1, 1].getxpos(); armtargety = gboard[1, 1].getypos();
                            movefound = true;
                        }
                        if (board[1, 1] == board[2, 2] && board[0, 0] == 0 && board[0, 0] != z)
                        {
                            armtargetx = gboard[0, 0].getxpos(); armtargety = gboard[0, 0].getypos();
                            movefound = true;
                        }

                        //check Diagonal (Bottom Right to top left)
                        if (board[0, 2] == board[1, 1] && board[2, 0] == 0 && board[1, 1] != z)
                        {
                            armtargetx = gboard[2, 0].getxpos(); armtargety = gboard[2, 0].getypos();
                            movefound = true;
                        }
                        if (board[0, 2] == board[2, 0] && board[1, 1] == 0 && board[0, 2] != z)
                        {
                            armtargetx = gboard[1, 1].getxpos(); armtargety = gboard[1, 1].getypos();
                            movefound = true;
                        }
                        if (board[2, 0] == board[1, 1] && board[0, 2] == 0 && board[1, 1] != z)
                        {
                            armtargetx = gboard[0, 2].getxpos(); armtargety = gboard[0, 2].getypos();
                            movefound = true;
                        }
                    }
                }
            //If computer goes first place a peice in a random corner of board
            if (turnNum == 1)
            {
                Random rand = new Random();
                int r = rand.Next(1, 5);
                if (r == 0) armtargetx = gboard[0, 0].getxpos(); armtargety = gboard[0, 0].getypos();
                if (r == 1) armtargetx = gboard[2, 2].getxpos(); armtargety = gboard[2, 2].getypos();
                if (r == 2) armtargetx = gboard[2, 0].getxpos(); armtargety = gboard[2, 0].getypos();
                if (r == 3) armtargetx = gboard[0, 2].getxpos(); armtargety = gboard[0, 2].getypos();
                movefound = true;
            }

            //If computer goes second and centre is not taken place a peice in the centre, else place a peice in one of the corners
            if (turnNum == 2 && board[1, 1] == 0)
            {
                armtargetx = gboard[1, 1].getxpos(); armtargety = gboard[1, 1].getypos();
                movefound = true;
            }
            if (turnNum == 2 && board[1, 1] != 0)
            {
                Random rand = new Random();
                int r = rand.Next(1, 5);
                if (r == 0) armtargetx = gboard[0, 0].getxpos(); armtargety = gboard[0, 0].getypos();
                if (r == 1) armtargetx = gboard[2, 2].getxpos(); armtargety = gboard[2, 2].getypos();
                if (r == 2) armtargetx = gboard[2, 0].getxpos(); armtargety = gboard[2, 0].getypos();
                if (r == 3) armtargetx = gboard[0, 2].getxpos(); armtargety = gboard[0, 2].getypos();
                movefound = true;
            }
            //Third Turn, if centre is taken then place peice in unocupiedcorner 
            if (turnNum == 3 && board[1, 1] == P_mark && board[0, 0] == 0)
            {
                armtargetx = gboard[0, 0].getxpos(); armtargety = gboard[0, 0].getypos();
                movefound = true;
            }
            if (turnNum == 3 && board[1, 1] == P_mark && board[2, 2] == 0)
            {
                armtargetx = gboard[2, 2].getxpos(); armtargety = gboard[2, 2].getypos();
                movefound = true;
            }
            //if centre is not taken look for a valid corner place to take
            if (turnNum == 3 && board[2, 2] == 0)
            {
                armtargetx = gboard[2, 2].getxpos(); armtargety = gboard[2, 2].getypos();
                movefound = true;
            }
            if (turnNum == 3 && board[0, 2] == 0)
            {
                armtargetx = gboard[0, 2].getxpos(); armtargety = gboard[0, 2].getypos();
                movefound = true;
            }
            if (turnNum == 3 && board[2, 0] == 0)
            {
                armtargetx = gboard[2, 0].getxpos(); armtargety = gboard[2, 0].getypos();
                movefound = true;
            }

            //turn 4, if opponent does not have 2 peices in one row then do whatever
            if (turnNum == 4) { }


            Console.WriteLine(armtargetx);
            Console.WriteLine(armtargety);

        }

        //Kinect Camera Code

        private void kinectstart()
        {
            // Look through all sensors and start the first connected one.

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {

                this.colorCoordinates = new ColorImagePoint[this.sensor.DepthStream.FramePixelDataLength];
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                this.colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];
                this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];
                this.colorBitmap = new WriteableBitmap(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                this.depthBitmap = new WriteableBitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);

            

                this.sensor.AllFramesReady += this.sensor_AllFramesReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();

                }
                catch (IOException)
                {
                    this.sensor = null;
                }

            }

        }


        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }



        public Bitmap BitmapFromSource(System.Windows.Media.Imaging.BitmapSource bitmapsource)
        {
            //convert image format
            var src = new System.Windows.Media.Imaging.FormatConvertedBitmap();
            src.BeginInit();
            src.Source = bitmapsource;
            src.DestinationFormat = System.Windows.Media.PixelFormats.Bgra32;
            src.EndInit();

            //copy to bitmap
            Bitmap bitmap = new Bitmap(src.PixelWidth, src.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var data = bitmap.LockBits(new Rectangle(System.Drawing.Point.Empty, bitmap.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            src.CopyPixels(System.Windows.Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bitmap.UnlockBits(data);

            return bitmap;
        }

       
        //When sensor is ready start stream and proces information
        public void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            BitmapSource clrimg = null;
            int Timeout = 0;


            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {

                if (depthFrame != null)
                {
                    depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);
                    this.depthBitmap.WritePixels(
                                               new Int32Rect(0, 0, this.depthBitmap.PixelWidth, this.depthBitmap.PixelHeight),
                                               this.depthPixels,
                                               this.depthBitmap.PixelWidth * sizeof(int),
                                               0);
                }

            }

            using (ColorImageFrame imageFrame = e.OpenColorImageFrame())
            {
               

                    if (imageFrame != null)
                    {

                        imageFrame.CopyPixelDataTo(this.colorPixels);
                        this.colorBitmap.WritePixels(
                            new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                            this.colorPixels,
                            this.colorBitmap.PixelWidth * sizeof(int),
                            0);
                        clrimg = colorBitmap;

                        //convert image into something opencv can use
                        Image<Bgr, byte> Frame = new Image<Bgr, byte>(BitmapFromSource(clrimg));

                        mark marks = new mark();

                        //hsv image 
                        Mat HsvImage = new Mat();

                        //threshold image for marks
                        Mat threshold = new Mat();

                        //threshold image for board
                        Mat threshold2 = new Mat();
                        //thresh image for peices
                        Mat threshold3 = new Mat();
                        
                        //creating HsvImage 
                        CvInvoke.CvtColor(Frame, HsvImage, ColorConversion.Bgr2Hsv);

                        //if user wants to calibrate mark button is pushed
                        if (markcalib)
                        {


                            CvInvoke.InRange(HsvImage, new ScalarArray(new MCvScalar(Trackbars.minhue, Trackbars.minsat, Trackbars.minval)), new ScalarArray(new MCvScalar(Trackbars.maxhue, Trackbars.maxsat, Trackbars.maxval)), threshold3);
                            morphOps(threshold3);
                            CvInvoke.Imshow("thresholdedImage3", threshold3);

                            mhmin = Trackbars.minhue; msmin = Trackbars.minsat; mvmin = Trackbars.minval;
                            mhmax = Trackbars.maxhue; msmax = Trackbars.maxsat; mvmax = Trackbars.maxval;
                        }

                        //once complete set mark hsv values
                        if (markcalibed)
                        {
                            markcalib = false;
                            CvInvoke.DestroyWindow("thresholdedImage3");
                            //Trackbars.Visible = false;
                            CvInvoke.InRange(HsvImage, new ScalarArray(new MCvScalar(bhmin, bsmin, bvmin)), new ScalarArray(new MCvScalar(bhmax, bsmax, bvmax)), threshold);
                            morphOps(threshold);
                        }

                        //preset markcalibration values
                        else
                        {
                            CvInvoke.InRange(HsvImage, new ScalarArray(new MCvScalar(165, 126, 38)), new ScalarArray(new MCvScalar(225, 223, 110)), threshold);
                            morphOps(threshold);
                        }

                        //if board calibrate button is pushed
                        if (boardcalib)
                        {


                            CvInvoke.InRange(HsvImage, new ScalarArray(new MCvScalar(Trackbars.minhue, Trackbars.minsat, Trackbars.minval)), new ScalarArray(new MCvScalar(Trackbars.maxhue, Trackbars.maxsat, Trackbars.maxval)), threshold2);
                            morphOps(threshold2);
                            bhmin = Trackbars.minhue; bsmin = Trackbars.minsat; bvmin = Trackbars.minval;
                            bhmax = Trackbars.maxhue; bsmax = Trackbars.maxsat; bvmax = Trackbars.maxval;

                            CvInvoke.Imshow("thresholdedImage2", threshold2);
                        }

                        //after calibration use calibrated values
                        if (boardcalibed)
                        {
                            boardcalib = false;
                            CvInvoke.DestroyWindow("thresholdedImage2");

                            CvInvoke.InRange(HsvImage, new ScalarArray(new MCvScalar(bhmin, bsmin, bvmin)), new ScalarArray(new MCvScalar(bhmax, bsmax, bvmax)), threshold2);
                            morphOps(threshold2);
                        }

                        //For board calibrate values manually
                        else
                        {

                            CvInvoke.InRange(HsvImage, new ScalarArray(new MCvScalar(94, 57, 66)), new ScalarArray(new MCvScalar(117, 172, 163)), threshold2);
                            morphOps(threshold2);
                        }


                        //find a gameboard
                        if (boardfind)
                        {
                            if (!boardfound)
                                findboard(threshold2, HsvImage, Frame);
                        }

                        if (tracking)
                        {
                            track(marks, threshold, HsvImage, Frame, depthPixels);
                        }

                        //Show startup box and do gamestart function
                        if (start)
                        {
                            StartupBox.Visible = true;
                            StartupBox.TopMost = true;

                        }


                   




                        //convert to something we can use

                        //Image<Bgr, Byte> yeah = Mat.ToImage<Bgr, Byte>();

                        //Console.WriteLine(gboard[0,0].getxpos());
                        Image1.Image = Frame;

                        //show board/ peices when button clicked
                        if (showboard) CvInvoke.Imshow("BoardThresholdedImage", threshold2);

                        if (showmarks) CvInvoke.Imshow("MarksThresholdedImage", threshold);

                    //CvInvoke.InRange(openCv, new ScalarArray(new MCvScalar(0, 126, 116)), new ScalarArray(new MCvScalar(214, 256, 256)), threshold);

                    //CvInvoke.cvReleaseMat(ref IntPtr threshold2);
                    //CvInvoke.WaitKey(100);

                }
                    //Timeout++;
                    //Destroy memory every x milliseconds
                    //if (Timeout == 3000)
                    //{
                    //    CvInvoke.DestroyAllWindows();
                    //    Timeout = 0;
                    //}
                    
                
            }
        }



        //Robot Target Function
        private void RobTarg(List<mark> themarks)
        {
            RobotScan.Targ = true;

        }

        //finding depth readings co ords for arm
        private void depthfunction(List<mark> themarks, DepthImagePixel[] depthPixels)
        {
            
                    DepthImagePixel[] depthData = new DepthImagePixel[640 * 480]; // Data from the Depth Frame
                    DepthImagePoint[] result = new DepthImagePoint[640 * 480];

            for (int i = 0; i < themarks.Count(); i++)
            {

                //look for a mark that is the same type as the computers mark
                if (themarks.ElementAt(i).gettype() == C_mark)
                {
                    
                    //look for a mark that isnt on the board already
                    //if it is more than 3 board lengths away from centre
                 
                    if (Math.Abs(gboard[1, 1].getxpos() - themarks.ElementAt(i).getxpos()) >= BOARD_SIZE * 2|| Math.Abs(gboard[1, 1].getypos() - themarks.ElementAt(i).getypos()) >= BOARD_SIZE*2)
                    {
                        
                            //point cloud mapping
                            this.sensor.CoordinateMapper.MapDepthFrameToColorFrame(DepthImageFormat.Resolution640x480Fps30,
                            this.depthPixels, ColorImageFormat.RgbResolution640x480Fps30, this.colorCoordinates);

                            //coordinate mapping of color frame to depth frame
                            //sensor.CoordinateMapper.MapColorFrameToDepthFrame(ColorImageFormat.RgbResolution640x480Fps30, 
                            //DepthImageFormat.Resolution640x480Fps30, depthData, result);

                            //    double Depthv1 = result[themarks[0].getxpos() * themarks[0].getypos()].Depth;
                            //    Console.WriteLine(Depthv1);

                            //depthpoint, ColorImageFormat.RgbResolution640x480Fps30);

                            //depth Index
                            //int depthIndex = themarks[0].getxpos() + (themarks[0].getypos() * 640);
                            //DepthImagePixel depthPixel = this.depthPixels[depthIndex];

                            // scale color coordinates to depth resolution

                            //int X=colorCoordinates.X/20;
                            //int Y = ColorImagePoint.Y/20;



                            short depth = depthPixels[themarks[i].getxpos() * themarks[i].getypos()].Depth;
                            int colorx = colorCoordinates[themarks[i].getxpos()].X;
                            int colory = colorCoordinates[themarks[i].getypos()].X;

                            Console.WriteLine(depth);

                            //x distance from the centre of the image (mm) (camera Perspective)
                            double realx = (colorx - 320) * 0.00173667 * depth;
                            double realy = (colory - 320) * 0.00173667 * depth;

                            //Coordinates adjusted relative to centre of board


                            Console.WriteLine(realx);
                            //Console.WriteLine(themarks[0].getxpos());
                            // Console.WriteLine(colorx);



                            //exit loop
                            i = themarks.Count() + 1;
                        
                    }
                }
            }

        }
            
        



//findboardfunction
private void findboard(Mat threshold2, Mat HSV, Image<Bgr, byte> Frame)
        {
            
            //create temporary image
            
            Mat temp = new Mat();
     
            threshold2.CopyTo(temp);

            //set gboard paramaters
            
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

            //find contours of filtered image using openCV findContours function
            CvInvoke.FindContours(temp, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
            int count = contours.Size;
            for (int z = 0; z < count; z++)
            {

                using (VectorOfPoint contour = contours[z])
                using (VectorOfPoint approxContour = new VectorOfPoint())
                {
                    CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                    if (CvInvoke.ContourArea(approxContour, false) > 800) //only consider contours with area greater than 400
                    {
                        
                            //check to see if board is found
                            if (approxContour.Size == 4)
                            {


                            //find the length of each square and set their size ((board perimeter / 4) / 3)
                                for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    gboard[i, j].setsize(Convert.ToInt32((CvInvoke.ArcLength(contours[z], false)) / 12));
                    BOARD_SIZE = (Convert.ToInt32((CvInvoke.ArcLength(contours[z], false)) / 12));
                }
            }

                            MCvMoments moments = CvInvoke.Moments(contours[z]);
                            double area = moments.M00;
                            boardfind = false;
                            boardfound = true;

                            //find centre of top left square
                            boardxorigin = (Convert.ToInt32(moments.M10 / area) - gboard[0, 0].getsize());
                            boardyorigin = (Convert.ToInt32(moments.M01 / area) - gboard[0, 0].getsize());

                            //set all other board square centre x and y pixel positions (assuming same place)
                            //for some reason y positions increase as camera pixels get lower!
                            for (int i = 0; i <= 2; i++)
                            {
                                gboard[i, 0].setxpos(boardxorigin);
                                gboard[0, i].setypos(boardyorigin);

                                for (int j = 0; j <= 2; j++)
                                {
                                    gboard[i, j].setxpos(gboard[0, 0].getxpos() + (j * BOARD_SIZE));
                                    gboard[i, j].setypos(gboard[0, 0].getypos() + (i * BOARD_SIZE));
                                }

                            }

                            //real world board dimensions for a 300mmx300mm board(each pixel size or square size)
                            //gboard[0, 0].setpsize(100 / (gboard[0, 0].getsize()));
                        }
        //set boardfind to be true
       
                       
                        }
                    }
                }

            //Following code is to find camera angles
            #region
            //to find camera angles we will find the real world distances of 2 points on the gameboard and
            //perform some mathematic operations to find the angle of the camera (we can assume that the camera
            //will remain relatively horizontal so only Y values will be used
            //DepthImagePixel[] depthData = new DepthImagePixel[640 * 480]; // Data from the Depth Frame
            //DepthImagePoint[] result = new DepthImagePoint[640 * 480];

            this.sensor.CoordinateMapper.MapDepthFrameToColorFrame(DepthImageFormat.Resolution640x480Fps30,
            this.depthPixels, ColorImageFormat.RgbResolution640x480Fps30, this.colorCoordinates);

            //
            int depthIndex1 = gboard[0, 1].getxpos() + (gboard[0, 1].getypos() * 640);
            int depthIndex2= gboard[2, 1].getxpos() + (gboard[2, 1].getypos() * 640);

            //depth values for both points
            int depth1 = this.depthPixels[depthIndex1].Depth;
            int depth2 = this.depthPixels[depthIndex2].Depth;

            //adjusted Y pixels
            int colory1 = colorCoordinates[depthIndex1].Y;
            int colory2= colorCoordinates[depthIndex2].Y;

            //depth of Y values of board
            //short depth1 = depthPixels[gboard[0, 1].getxpos()*gboard[0,1].getypos()].Depth;
            //short depth2 = depthPixels[gboard[2, 1].getxpos()*gboard[2,1].getypos()].Depth;

            //adjusted color pixels positions
            //int colory1 = colorCoordinates[gboard[0, 1].getxpos() * gboard[0, 1].getypos()].Y;
            //int colory2 = colorCoordinates[gboard[2, 1].getxpos() * gboard[2, 1].getypos()].Y;
            
            
            //real world y distances from the centre of the image (the constant is from established values for kinect) (mm)
            double realy1 = (colory1 - 240) * 0.001707 * depth1;
            double realy2 = (colory2 - 240) * 0.001707 * depth2;

            //calculating the size of a pixel based on the distance
            
            //now Performing basic triginometry (knowing that the true size between these two points
            //is 200mm)

            angle=Math.Acos((200)/ (realy2 - realy1));

            #endregion

            if (boardfound)
            {
                Console.WriteLine("0,1 x"); Console.WriteLine(gboard[0, 1].getxpos());
                Console.WriteLine("0,1 y"); Console.WriteLine(gboard[0, 1].getypos());

                Console.WriteLine("1,1 x"); Console.WriteLine(gboard[1, 1].getxpos());
                Console.WriteLine("1,1 y"); Console.WriteLine(gboard[1, 1].getypos());

                Console.WriteLine("2,1 x"); Console.WriteLine(gboard[2, 1].getxpos());
                Console.WriteLine("2,1 y"); Console.WriteLine(gboard[2, 1].getypos());


                Console.WriteLine(BOARD_SIZE);
            
                Console.WriteLine(angle);
                Console.WriteLine(depth1);
                Console.WriteLine(depth2);
                Console.WriteLine(colory1);
                Console.WriteLine(colory2);
                Console.WriteLine(realy1);
                Console.WriteLine(realy2);

                MessageBox.Show("gameboard found ready to play");

                //CvInvoke.DestroyWindow("BoardthresholdImage");
            }
            if (!boardfound)
            {
                boardfind = false;
                MessageBox.Show("No gameboard found, adjust board/ hsv settings");
                
            }
        }



        //morphops function
        private void morphOps(Mat threshold)
        {
            Mat structuringElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(8, 8), new System.Drawing.Point(-1, -1));

            CvInvoke.Dilate(threshold, threshold, structuringElement, new System.Drawing.Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 0, 0));
            CvInvoke.Erode(threshold, threshold, structuringElement, new System.Drawing.Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 0, 0));
        }

        //object tracking function
        private void track(mark themarks, Mat threshold, Mat HsvImage, Image<Bgr, byte> Frame, DepthImagePixel[] depthPixels)
        {
            CvInvoke.PutText(Frame, "Tracking Started", new System.Drawing.Point(0, 50), FontFace.HersheyComplex, 1, new Bgr(0, 255, 0).MCvScalar, 2);
            //create temporary image
            Mat temp = new Mat();
            threshold.CopyTo(temp);
            List<mark> peices = new List<mark>();
            bool objectfound = false;

            //vectors for Findcontours

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

            //find contours of filtered image using openCV findContours function
            CvInvoke.FindContours(temp, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
            int count = contours.Size;
            for (int i = 0; i < count; i++)
            {
                //marks
                mark peice = new mark();
                using (VectorOfPoint contour = contours[i])
                using (VectorOfPoint approxContour = new VectorOfPoint())
                {
                    CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                    
                    if (CvInvoke.ContourArea(approxContour, false) > 400) //only consider contours with area greater than 400
                    {

                        if (approxContour.Size == 3) //The contour has 3 vertices, it is a triangle
                        {
                            System.Drawing.Point[] pts = approxContour.ToArray();
                            MCvMoments moments = CvInvoke.Moments(contours[i]);
                            double area = moments.M00;

                            //objectFound = true;

                            peice.setxpos(Convert.ToInt32(moments.M10 / area));
                            peice.setypos(Convert.ToInt32(moments.M01 / area));

                            //check to see what position the peice is in
                            for (int j = 0; j <= 2; j++)
                            {
                                for (int k = 0; k <= 2; k++)
                                {
                                    //if its x position is within a half of square size pixels then boardx and y are 
                                    if (Math.Abs(gboard[j, k].getxpos() - peice.getxpos()) <= BOARD_SIZE / 2)
                                    {

                                        if (Math.Abs(gboard[j, k].getypos() - peice.getypos()) <= BOARD_SIZE / 2)
                                        {
                                            peice.setboardx(k);
                                            peice.setboardy(j);
                                            //Console.WriteLine(peice.getboardx());
                                            //Console.WriteLine(peice.getboardx());
                                        }
                                    }


                                }
                            }
                                //set peice type to triangle (1)
                            peice.settype(1);
                            
                            peices.Add(peice);
                            objectfound = true;
                        }

                        else if (approxContour.Size == 4) //The contour has 4 vertices.
                        {
                            #region determine if all the angles in the contour are within [80, 100] degree
                            bool isRectangle = true;
                            System.Drawing.Point[] pts = approxContour.ToArray();
                            LineSegment2D[] edges = Emgu.CV.PointCollection.PolyLine(pts, true);

                            for (int j = 0; j < edges.Length; j++)
                            {
                                double angle = Math.Abs(
                                   edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                if (angle < 80 || angle > 100)
                                {
                                    isRectangle = false;
                                    break;
                                }
                            }
                            #endregion

                            if (isRectangle)
                            {

                                MCvMoments moments = CvInvoke.Moments(contours[i]);
                                double area = moments.M00;
                                peice.setxpos(Convert.ToInt32(moments.M10 / area));
                                peice.setypos(Convert.ToInt32(moments.M01 / area));
                                //Console.WriteLine(peice.getxpos());

                                //Console.WriteLine(peice.getxpos());
                                //Console.WriteLine(peice.getypos());
                                //set peice board positions
                                for (int j = 0; j <= 2; j++)
                                {
                                    for (int k = 0; k <= 2; k++)
                                    {
                                        //if its x position is within a half of square size pixels then boardx and y are 
                                        if (Math.Abs(gboard[j, k].getxpos() - peice.getxpos()) <= BOARD_SIZE/2)
                                        {

                                            if (Math.Abs(gboard[j, k].getypos() - peice.getypos()) <= BOARD_SIZE/2)
                                            {
                                                peice.setboardx(k);
                                                peice.setboardy(j);
                                                //Console.WriteLine(peice.getboardx());
                                            }
                                        }
                                       
                                            
                                        }
                                    

                                }
                                //set peice type to square
                                peice.settype(2);
                                peices.Add(peice);
                                objectfound = true;
                                armtargetx = peice.getxpos();
                                armtargety = peice.getypos();

                            }
                        }
                    }
                }
            }

           


            //Console.WriteLine(peices.ElementAt(1).getboardx());
            if (objectfound)
            {
                //Drawingfunction to show peices
                draw(peices, Frame);
                //check to see if board state has changed after player locks in move
                if (Lockin) boardchange(peices);

                depthfunction(peices, depthPixels);
            }

            if (turn)
            {
                //if its arms turn then figure out where to move
                strategy();
            }



            //find depth values for returned targets
          
            
            }

        //drawing function
        private void draw(List<mark> themarks, Image<Bgr, byte> Frame)
        {
            for (int i = 0; i < themarks.Count(); i++)
            {
                CvInvoke.PutText(Frame, "Tracking Object", new System.Drawing.Point(0, 50), FontFace.HersheyComplex, 1, new Bgr(0, 255, 0).MCvScalar, 2);
                //draw object location on screen
                CvInvoke.Circle(Frame, new System.Drawing.Point(themarks.ElementAt(i).getxpos(), themarks.ElementAt(i).getypos()), 10, new Bgr(0, 0, 255).MCvScalar);

                if (themarks.ElementAt(i).gettype() == 2)
                {
                    CvInvoke.PutText(Frame, "square", new System.Drawing.Point(themarks.ElementAt(i).getxpos(), themarks.ElementAt(i).getypos()), FontFace.HersheyComplex, 1, new Bgr(0, 255, 0).MCvScalar, 2);
                }
                if (themarks.ElementAt(i).gettype() == 1)
                {
                    CvInvoke.PutText(Frame, "triangle", new System.Drawing.Point(themarks.ElementAt(i).getxpos(), themarks.ElementAt(i).getypos()), FontFace.HersheyComplex, 1, new Bgr(0, 255, 0).MCvScalar, 2);
                }
            }
        }


        //boardstate changes
        private void boardchange(List<mark> themarks)
        {
            
            //set temporary board status then wait x second
            for (int j = 0; j <= 2; j++)
            {
                for (int k = 0; k <= 2; k++)
                {
                    board[j, k] = gboard[j, k].getstate();
                }
            }

            //nested loops to check board positions of each mark
            for (int i = 0; i < themarks.Count(); i++)
            {

                for (int j = 0; j <= 2; j++)
                {
                    for (int k = 0; k <= 2; k++)
                    {

                        //if the marks position matches a board position change board state
                        if (themarks.ElementAt(i).getboardx() == (k) && themarks.ElementAt(i).getboardy() == (j))
                        {
                           
                            //if marks x and y pos match then set board state to 2(square) or 1(cross)
                            if (themarks.ElementAt(i).gettype() == 1)
                            {
                                gboard[j, k].setstate(1);
                                
                            }
                            if (themarks.ElementAt(i).gettype() == 2)
                            {
                                gboard[j, k].setstate(2);
                                
                            }

                        }
                    }
                }
            }

            //check a new frame to see if board has changed, if the board state is the same after 2 loops the program will continue
            for (int j = 0; j <= 2; j++)
            {
                for (int k = 0; k <= 2; k++)
                {
                    if (gboard[j, k].getstate() != board[j, k])
                        //exit function and return to main loop
                        //the board has changed 

                        boardchanged = true;
                        //goto stop;
                }
            }
            //if board state has changed player has made a move increase turnNum by 1;
            turnNum = turnNum + 1;


            //Print board results
            if (boardchanged){
                for (int j = 0; j <= 2; j++)
                {
                    for (int k = 0; k <= 2; k++)
                    {
                        Console.WriteLine(gboard[j, k].getstate());
                    }
                    Console.WriteLine("\n");
                }

                Console.WriteLine("\n");
                turn = true;
                boardchanged = false;
            }
            //goto marker
            //stop:
            //wait 3 seconds before taking new sample
            //CvInvoke.WaitKey(3000);
        }



        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }

        ////start button function
        private void Startbutton_Click(object sender, EventArgs e)
        {
            if (!gameinprogress)
            {
               
                start = true;
                
                Startbutton.Text = "Stop";
                gameinprogress = true;
                //tracking = true;

            }
           

            else
            {
               
                start = false;
                Startbutton.Text = "Start";
                gameinprogress = false;
            }
        }



        private void Trackingbtn_Click(object sender, EventArgs e)
        {
            if (boardfound)
            {
                if (!tracking)
                {
                    tracking = true;
                    Trackingbtn.Text = "Stop tracking";
                    
                }
                else
                {
                    tracking = false;

                    Trackingbtn.Text = "Start tracking";
                }

            }
            else
            {
                MessageBox.Show("You must Find a gameboard first");
            }
            }

        private void FindAGameBoard_Click(object sender, EventArgs e)
        {
            if (!boardfind)
            {
                boardfind = true;

            }
            else
            {

                boardfind = false;
            }



        }

        //for pushing calibrate board button
        private void calib_Click(object sender, EventArgs e)
        {
            Trackbars.Visible = true;
            Trackbars.TopMost = true;
            if (!boardcalib) boardcalib = true;
            else
            {
                boardcalib = false;
                boardcalibed = false;
                
            }

            }

        //for pushing mark calibration button
        private void button1_Click(object sender, EventArgs e)
        {
            Trackbars.Visible = true;
            Trackbars.TopMost = true;
            if (!markcalib) markcalib = true;
            else
            {
                markcalib = false;
                markcalibed = false;
            }
        }


        private void markbtn_Click(object sender, EventArgs e)
        {
            if (!showmarks)
            {
                showmarks = true;
                markbtn.Text = "Hide Peices Threshold Image";
               
            }

            else
            {
                showmarks = false;
                
                markbtn.Text = "Show Peices Threshold Image";
                CvInvoke.DestroyWindow("MarksThresholdedImage");
            }
        }

        private void boardbtn_Click(object sender, EventArgs e)
        {
            if (!showboard)
            {
                showboard = true;
                boardbtn.Text = "Hide Board Threshold Image";
            }

            else
            {
                showboard = false;
                boardbtn.Text = "Show Board Threshold Image";
                CvInvoke.DestroyWindow("BoardThresholdedImage");
            }
        }

        //lock in button
        private void lockin_Click(object sender, EventArgs e)
        {
            
                Lockin = true;
        }
    }     
}

       
 
   

