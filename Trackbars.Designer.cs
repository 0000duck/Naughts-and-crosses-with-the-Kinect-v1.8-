namespace CameraCapture
{
    partial class Trackbars
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
            this.huemin = new System.Windows.Forms.TrackBar();
            this.huemax = new System.Windows.Forms.TrackBar();
            this.satmin = new System.Windows.Forms.TrackBar();
            this.satmax = new System.Windows.Forms.TrackBar();
            this.valmin = new System.Windows.Forms.TrackBar();
            this.valmax = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.huenum = new System.Windows.Forms.Label();
            this.okbtn = new System.Windows.Forms.Button();
            this.huenum2 = new System.Windows.Forms.Label();
            this.satnum = new System.Windows.Forms.Label();
            this.satnum2 = new System.Windows.Forms.Label();
            this.valnum = new System.Windows.Forms.Label();
            this.valnum2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.huemin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huemax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.satmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.satmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valmax)).BeginInit();
            this.SuspendLayout();
            // 
            // huemin
            // 
            this.huemin.Location = new System.Drawing.Point(0, 33);
            this.huemin.Maximum = 255;
            this.huemin.Name = "huemin";
            this.huemin.Size = new System.Drawing.Size(413, 56);
            this.huemin.TabIndex = 0;
            this.huemin.Scroll += new System.EventHandler(this.huemin_Scroll);
            // 
            // huemax
            // 
            this.huemax.Location = new System.Drawing.Point(0, 95);
            this.huemax.Maximum = 255;
            this.huemax.Name = "huemax";
            this.huemax.Size = new System.Drawing.Size(423, 56);
            this.huemax.TabIndex = 1;
            this.huemax.Value = 255;
            this.huemax.Scroll += new System.EventHandler(this.huemax_Scroll);
            // 
            // satmin
            // 
            this.satmin.Location = new System.Drawing.Point(0, 157);
            this.satmin.Maximum = 255;
            this.satmin.Name = "satmin";
            this.satmin.Size = new System.Drawing.Size(413, 56);
            this.satmin.TabIndex = 2;
            this.satmin.Scroll += new System.EventHandler(this.satmin_Scroll);
            // 
            // satmax
            // 
            this.satmax.Location = new System.Drawing.Point(0, 219);
            this.satmax.Maximum = 255;
            this.satmax.Name = "satmax";
            this.satmax.Size = new System.Drawing.Size(413, 56);
            this.satmax.TabIndex = 3;
            this.satmax.Value = 255;
            this.satmax.Scroll += new System.EventHandler(this.satmax_Scroll);
            // 
            // valmin
            // 
            this.valmin.Location = new System.Drawing.Point(0, 281);
            this.valmin.Maximum = 255;
            this.valmin.Name = "valmin";
            this.valmin.Size = new System.Drawing.Size(423, 56);
            this.valmin.TabIndex = 4;
            this.valmin.Scroll += new System.EventHandler(this.valmin_Scroll);
            // 
            // valmax
            // 
            this.valmax.Location = new System.Drawing.Point(0, 332);
            this.valmax.Maximum = 255;
            this.valmax.Name = "valmax";
            this.valmax.Size = new System.Drawing.Size(413, 56);
            this.valmax.TabIndex = 5;
            this.valmax.Value = 255;
            this.valmax.Scroll += new System.EventHandler(this.valmax_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hue min/max";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Saturation min/max";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Value min/max";
            // 
            // huenum
            // 
            this.huenum.AutoSize = true;
            this.huenum.Location = new System.Drawing.Point(186, 62);
            this.huenum.Name = "huenum";
            this.huenum.Size = new System.Drawing.Size(16, 17);
            this.huenum.TabIndex = 9;
            this.huenum.Text = "0";
            this.huenum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // okbtn
            // 
            this.okbtn.Location = new System.Drawing.Point(208, 379);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(215, 35);
            this.okbtn.TabIndex = 10;
            this.okbtn.Text = "Ok";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // huenum2
            // 
            this.huenum2.AutoSize = true;
            this.huenum2.Location = new System.Drawing.Point(186, 122);
            this.huenum2.Name = "huenum2";
            this.huenum2.Size = new System.Drawing.Size(32, 17);
            this.huenum2.TabIndex = 12;
            this.huenum2.Text = "255";
            // 
            // satnum
            // 
            this.satnum.AutoSize = true;
            this.satnum.Location = new System.Drawing.Point(186, 199);
            this.satnum.Name = "satnum";
            this.satnum.Size = new System.Drawing.Size(16, 17);
            this.satnum.TabIndex = 13;
            this.satnum.Text = "0";
            // 
            // satnum2
            // 
            this.satnum2.AutoSize = true;
            this.satnum2.Location = new System.Drawing.Point(186, 261);
            this.satnum2.Name = "satnum2";
            this.satnum2.Size = new System.Drawing.Size(32, 17);
            this.satnum2.TabIndex = 14;
            this.satnum2.Text = "255";
            // 
            // valnum
            // 
            this.valnum.AutoSize = true;
            this.valnum.Location = new System.Drawing.Point(186, 312);
            this.valnum.Name = "valnum";
            this.valnum.Size = new System.Drawing.Size(16, 17);
            this.valnum.TabIndex = 15;
            this.valnum.Text = "0";
            // 
            // valnum2
            // 
            this.valnum2.AutoSize = true;
            this.valnum2.Location = new System.Drawing.Point(186, 359);
            this.valnum2.Name = "valnum2";
            this.valnum2.Size = new System.Drawing.Size(32, 17);
            this.valnum2.TabIndex = 16;
            this.valnum2.Text = "255";
            // 
            // Trackbars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 414);
            this.Controls.Add(this.valnum2);
            this.Controls.Add(this.valnum);
            this.Controls.Add(this.satnum2);
            this.Controls.Add(this.satnum);
            this.Controls.Add(this.huenum2);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.huenum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valmax);
            this.Controls.Add(this.valmin);
            this.Controls.Add(this.satmax);
            this.Controls.Add(this.satmin);
            this.Controls.Add(this.huemax);
            this.Controls.Add(this.huemin);
            this.Name = "Trackbars";
            this.Text = "Trackbars";
            this.Load += new System.EventHandler(this.Trackbars_Load);
            ((System.ComponentModel.ISupportInitialize)(this.huemin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huemax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.satmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.satmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valmax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar huemin;
        private System.Windows.Forms.TrackBar huemax;
        private System.Windows.Forms.TrackBar satmin;
        private System.Windows.Forms.TrackBar satmax;
        private System.Windows.Forms.TrackBar valmin;
        private System.Windows.Forms.TrackBar valmax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label huenum;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Label huenum2;
        private System.Windows.Forms.Label satnum;
        private System.Windows.Forms.Label satnum2;
        private System.Windows.Forms.Label valnum;
        private System.Windows.Forms.Label valnum2;
    }
}