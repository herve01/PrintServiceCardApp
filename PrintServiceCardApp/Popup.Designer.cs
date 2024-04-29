
namespace PrintServiceCardApp
{
    partial class Popup
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
            this.pnlCtnr = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlCtnr
            // 
            this.pnlCtnr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCtnr.Location = new System.Drawing.Point(0, 0);
            this.pnlCtnr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCtnr.Name = "pnlCtnr";
            this.pnlCtnr.Size = new System.Drawing.Size(544, 554);
            this.pnlCtnr.TabIndex = 0;
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 554);
            this.Controls.Add(this.pnlCtnr);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Popup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Popup";
            this.Load += new System.EventHandler(this.Popup_Load);
            this.Resize += new System.EventHandler(this.Popup_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCtnr;
    }
}