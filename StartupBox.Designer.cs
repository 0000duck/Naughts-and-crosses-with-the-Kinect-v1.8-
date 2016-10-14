namespace CameraCapture
{
    partial class StartupBox
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
            this.squaresbtn = new System.Windows.Forms.Button();
            this.trianglesbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // squaresbtn
            // 
            this.squaresbtn.Location = new System.Drawing.Point(12, 174);
            this.squaresbtn.Name = "squaresbtn";
            this.squaresbtn.Size = new System.Drawing.Size(167, 41);
            this.squaresbtn.TabIndex = 0;
            this.squaresbtn.Text = "Squares";
            this.squaresbtn.UseVisualStyleBackColor = true;
            this.squaresbtn.Click += new System.EventHandler(this.squaresbtn_Click);
            // 
            // trianglesbtn
            // 
            this.trianglesbtn.Location = new System.Drawing.Point(306, 174);
            this.trianglesbtn.Name = "trianglesbtn";
            this.trianglesbtn.Size = new System.Drawing.Size(178, 41);
            this.trianglesbtn.TabIndex = 1;
            this.trianglesbtn.Text = "Triangles";
            this.trianglesbtn.UseVisualStyleBackColor = true;
            this.trianglesbtn.Click += new System.EventHandler(this.trianglesbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(424, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hello, lets play a game! Would you like to be squares or triangles?";
            // 
            // StartupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 227);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trianglesbtn);
            this.Controls.Add(this.squaresbtn);
            this.Name = "StartupBox";
            this.Text = "StartupBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button squaresbtn;
        private System.Windows.Forms.Button trianglesbtn;
        private System.Windows.Forms.Label label1;
    }
}