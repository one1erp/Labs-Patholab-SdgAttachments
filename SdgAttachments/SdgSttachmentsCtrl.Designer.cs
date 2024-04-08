namespace SdgAttachments
{
    partial class SdgSttachmentsCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton1
            // 
            this.radButton1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.radButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.radButton1.Location = new System.Drawing.Point(1, 1);
            this.radButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radButton1.Name = "radButton1";
            // 
            // 
            // 
            this.radButton1.RootElement.AccessibleDescription = null;
            this.radButton1.RootElement.AccessibleName = null;
            this.radButton1.RootElement.ControlBounds = new System.Drawing.Rectangle(1, 1, 110, 24);
            this.radButton1.Size = new System.Drawing.Size(98, 29);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "מסמכים נוספים";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // SdgSttachmentsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radButton1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SdgSttachmentsCtrl";
            this.Size = new System.Drawing.Size(101, 32);
            this.Load += new System.EventHandler(this.SdgSttachmentsCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
     //   private Telerik.WinControls.RootRadElement object_741d4b3c_de57_4425_9372_9031e72984dd;
    }
}
