
namespace PrintServiceCardApp.Modules.Template
{
    partial class ReportView
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.crReportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crReportView
            // 
            this.crReportView.ActiveViewIndex = -1;
            this.crReportView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crReportView.Cursor = System.Windows.Forms.Cursors.Default;
            this.crReportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crReportView.Location = new System.Drawing.Point(0, 0);
            this.crReportView.Name = "crReportView";
            this.crReportView.Size = new System.Drawing.Size(1056, 546);
            this.crReportView.TabIndex = 0;
            // 
            // ReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.crReportView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReportView";
            this.Size = new System.Drawing.Size(1056, 546);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crReportView;
    }
}
