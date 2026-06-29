namespace Local_Supermarket_Management_System
{
    partial class Home
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
            this.flowHome = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowHome
            // 
            this.flowHome.BackColor = System.Drawing.Color.LavenderBlush;
            this.flowHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowHome.Location = new System.Drawing.Point(0, 0);
            this.flowHome.Name = "flowHome";
            this.flowHome.Size = new System.Drawing.Size(834, 680);
            this.flowHome.TabIndex = 0;
            this.flowHome.Paint += new System.Windows.Forms.PaintEventHandler(this.flowHome_Paint);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowHome);
            this.Name = "Home";
            this.Size = new System.Drawing.Size(834, 680);
            this.Load += new System.EventHandler(this.Home_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowHome;
    }
}
