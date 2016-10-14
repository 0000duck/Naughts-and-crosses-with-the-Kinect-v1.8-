namespace CameraCapture
{
    partial class CameraCapture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Image1 = new Emgu.CV.UI.ImageBox();
            this.Startbutton = new System.Windows.Forms.Button();
            this.FindAGameBoard = new System.Windows.Forms.Button();
            this.Trackingbtn = new System.Windows.Forms.Button();
            this.calib = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.boardbtn = new System.Windows.Forms.Button();
            this.markbtn = new System.Windows.Forms.Button();
            this.lockin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.SuspendLayout();
            // 
            // Image1
            // 
            this.Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Image1.Location = new System.Drawing.Point(101, 3);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(869, 530);
            this.Image1.TabIndex = 2;
            this.Image1.TabStop = false;
            // 
            // Startbutton
            // 
            this.Startbutton.Location = new System.Drawing.Point(459, 539);
            this.Startbutton.Name = "Startbutton";
            this.Startbutton.Size = new System.Drawing.Size(220, 35);
            this.Startbutton.TabIndex = 3;
            this.Startbutton.Text = "Start";
            this.Startbutton.UseVisualStyleBackColor = true;
            this.Startbutton.Click += new System.EventHandler(this.Startbutton_Click);
            // 
            // FindAGameBoard
            // 
            this.FindAGameBoard.Location = new System.Drawing.Point(459, 580);
            this.FindAGameBoard.Name = "FindAGameBoard";
            this.FindAGameBoard.Size = new System.Drawing.Size(220, 34);
            this.FindAGameBoard.TabIndex = 4;
            this.FindAGameBoard.Text = "Find Board";
            this.FindAGameBoard.UseVisualStyleBackColor = true;
            this.FindAGameBoard.Click += new System.EventHandler(this.FindAGameBoard_Click);
            // 
            // Trackingbtn
            // 
            this.Trackingbtn.Location = new System.Drawing.Point(715, 539);
            this.Trackingbtn.Name = "Trackingbtn";
            this.Trackingbtn.Size = new System.Drawing.Size(277, 35);
            this.Trackingbtn.TabIndex = 5;
            this.Trackingbtn.Text = "Start Tracking";
            this.Trackingbtn.UseVisualStyleBackColor = true;
            this.Trackingbtn.Click += new System.EventHandler(this.Trackingbtn_Click);
            // 
            // calib
            // 
            this.calib.Location = new System.Drawing.Point(12, 539);
            this.calib.Name = "calib";
            this.calib.Size = new System.Drawing.Size(205, 35);
            this.calib.TabIndex = 6;
            this.calib.Text = "Calibrate Board HSV";
            this.calib.UseVisualStyleBackColor = true;
            this.calib.Click += new System.EventHandler(this.calib_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 580);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "Calibrate Peice HSV";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // boardbtn
            // 
            this.boardbtn.Location = new System.Drawing.Point(223, 580);
            this.boardbtn.Name = "boardbtn";
            this.boardbtn.Size = new System.Drawing.Size(206, 34);
            this.boardbtn.TabIndex = 8;
            this.boardbtn.Text = "Show Board Thresheld Image";
            this.boardbtn.UseVisualStyleBackColor = true;
            this.boardbtn.Click += new System.EventHandler(this.boardbtn_Click);
            // 
            // markbtn
            // 
            this.markbtn.Location = new System.Drawing.Point(223, 539);
            this.markbtn.Name = "markbtn";
            this.markbtn.Size = new System.Drawing.Size(206, 35);
            this.markbtn.TabIndex = 9;
            this.markbtn.Text = "Show Peices Thresheld Image";
            this.markbtn.UseVisualStyleBackColor = true;
            this.markbtn.Click += new System.EventHandler(this.markbtn_Click);
            // 
            // lockin
            // 
            this.lockin.Location = new System.Drawing.Point(715, 580);
            this.lockin.Name = "lockin";
            this.lockin.Size = new System.Drawing.Size(277, 34);
            this.lockin.TabIndex = 10;
            this.lockin.Text = "Lock in Move";
            this.lockin.UseVisualStyleBackColor = true;
            this.lockin.Click += new System.EventHandler(this.lockin_Click);
            // 
            // CameraCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 652);
            this.Controls.Add(this.lockin);
            this.Controls.Add(this.markbtn);
            this.Controls.Add(this.boardbtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.calib);
            this.Controls.Add(this.Trackingbtn);
            this.Controls.Add(this.FindAGameBoard);
            this.Controls.Add(this.Startbutton);
            this.Controls.Add(this.Image1);
            this.Name = "CameraCapture";
            this.Text = "Camera Output";
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox Image1;
        private System.Windows.Forms.Button Startbutton;
        private System.Windows.Forms.Button FindAGameBoard;
        private System.Windows.Forms.Button Trackingbtn;
        private System.Windows.Forms.Button calib;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button boardbtn;
        private System.Windows.Forms.Button markbtn;
        private System.Windows.Forms.Button lockin;
    }
}

