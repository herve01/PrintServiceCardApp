
namespace PrintServiceCardApp
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFonction = new System.Windows.Forms.Button();
            this.btnGrade = new System.Windows.Forms.Button();
            this.btnCard = new System.Windows.Forms.Button();
            this.signMenu = new System.Windows.Forms.Panel();
            this.pnlCtnrView = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnFonction);
            this.panel1.Controls.Add(this.btnGrade);
            this.panel1.Controls.Add(this.btnCard);
            this.panel1.Controls.Add(this.signMenu);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 713);
            this.panel1.TabIndex = 0;
            // 
            // btnFonction
            // 
            this.btnFonction.FlatAppearance.BorderSize = 0;
            this.btnFonction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFonction.Location = new System.Drawing.Point(10, 161);
            this.btnFonction.Name = "btnFonction";
            this.btnFonction.Size = new System.Drawing.Size(200, 31);
            this.btnFonction.TabIndex = 3;
            this.btnFonction.Text = "  Fonction";
            this.btnFonction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFonction.UseVisualStyleBackColor = true;
            this.btnFonction.Click += new System.EventHandler(this.btnFonction_Click);
            // 
            // btnGrade
            // 
            this.btnGrade.FlatAppearance.BorderSize = 0;
            this.btnGrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrade.Location = new System.Drawing.Point(10, 126);
            this.btnGrade.Name = "btnGrade";
            this.btnGrade.Size = new System.Drawing.Size(200, 31);
            this.btnGrade.TabIndex = 2;
            this.btnGrade.Text = "  Grade";
            this.btnGrade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrade.UseVisualStyleBackColor = true;
            this.btnGrade.Click += new System.EventHandler(this.btnGrade_Click);
            // 
            // btnCard
            // 
            this.btnCard.FlatAppearance.BorderSize = 0;
            this.btnCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCard.Location = new System.Drawing.Point(10, 90);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(200, 31);
            this.btnCard.TabIndex = 1;
            this.btnCard.Text = "  Carte de service";
            this.btnCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCard.UseVisualStyleBackColor = true;
            this.btnCard.Click += new System.EventHandler(this.btnListe_Click);
            // 
            // signMenu
            // 
            this.signMenu.BackColor = System.Drawing.Color.CornflowerBlue;
            this.signMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.signMenu.Location = new System.Drawing.Point(4, 90);
            this.signMenu.Name = "signMenu";
            this.signMenu.Size = new System.Drawing.Size(5, 33);
            this.signMenu.TabIndex = 0;
            // 
            // pnlCtnrView
            // 
            this.pnlCtnrView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCtnrView.Location = new System.Drawing.Point(219, 1);
            this.pnlCtnrView.Name = "pnlCtnrView";
            this.pnlCtnrView.Size = new System.Drawing.Size(1056, 710);
            this.pnlCtnrView.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel2.Location = new System.Drawing.Point(10, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 1);
            this.panel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel4.Location = new System.Drawing.Point(10, 193);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 1);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel5.Location = new System.Drawing.Point(10, 158);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 1);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel6.Location = new System.Drawing.Point(10, 88);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 1);
            this.panel6.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 711);
            this.Controls.Add(this.pnlCtnrView);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlCtnrView;
        private System.Windows.Forms.Button btnFonction;
        private System.Windows.Forms.Button btnGrade;
        private System.Windows.Forms.Button btnCard;
        private System.Windows.Forms.Panel signMenu;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
    }
}

