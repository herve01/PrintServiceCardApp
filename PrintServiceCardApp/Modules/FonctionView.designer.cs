﻿
namespace PrintServiceCardApp.Modules
{
    partial class FonctionView
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
            this.lstViewData = new System.Windows.Forms.ListView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtResearch = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.cmbGrade = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlZone = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIntitule = new System.Windows.Forms.TextBox();
            this.btnRefreshD = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlZone.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstViewData
            // 
            this.lstViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstViewData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstViewData.HideSelection = false;
            this.lstViewData.Location = new System.Drawing.Point(384, 51);
            this.lstViewData.Margin = new System.Windows.Forms.Padding(2);
            this.lstViewData.Name = "lstViewData";
            this.lstViewData.Size = new System.Drawing.Size(475, 615);
            this.lstViewData.TabIndex = 1;
            this.lstViewData.UseCompatibleStateImageBehavior = false;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.Location = new System.Drawing.Point(17, 247);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Enregistrer";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Grade";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(7, 139);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(333, 74);
            this.txtDescription.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(233, 247);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtResearch
            // 
            this.txtResearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtResearch.Location = new System.Drawing.Point(442, 13);
            this.txtResearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtResearch.Name = "txtResearch";
            this.txtResearch.Size = new System.Drawing.Size(318, 25);
            this.txtResearch.TabIndex = 7;
            this.txtResearch.Tag = "Recherche....";
            this.txtResearch.TextChanged += new System.EventHandler(this.txtResearch_TextChanged);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblCount.Location = new System.Drawing.Point(764, 14);
            this.lblCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(29, 21);
            this.lblCount.TabIndex = 8;
            this.lblCount.Text = "(1)";
            // 
            // cmbGrade
            // 
            this.cmbGrade.BackColor = System.Drawing.SystemColors.Control;
            this.cmbGrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGrade.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbGrade.FormattingEnabled = true;
            this.cmbGrade.Location = new System.Drawing.Point(6, 33);
            this.cmbGrade.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGrade.Name = "cmbGrade";
            this.cmbGrade.Size = new System.Drawing.Size(293, 25);
            this.cmbGrade.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(4, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Description";
            // 
            // pnlZone
            // 
            this.pnlZone.Controls.Add(this.label2);
            this.pnlZone.Controls.Add(this.txtIntitule);
            this.pnlZone.Controls.Add(this.btnRefreshD);
            this.pnlZone.Controls.Add(this.label3);
            this.pnlZone.Controls.Add(this.cmbGrade);
            this.pnlZone.Controls.Add(this.txtDescription);
            this.pnlZone.Controls.Add(this.label1);
            this.pnlZone.Location = new System.Drawing.Point(10, 11);
            this.pnlZone.Margin = new System.Windows.Forms.Padding(2);
            this.pnlZone.Name = "pnlZone";
            this.pnlZone.Size = new System.Drawing.Size(345, 224);
            this.pnlZone.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(3, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 19);
            this.label2.TabIndex = 28;
            this.label2.Text = "Intitule *";
            // 
            // txtIntitule
            // 
            this.txtIntitule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIntitule.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIntitule.Location = new System.Drawing.Point(6, 86);
            this.txtIntitule.Margin = new System.Windows.Forms.Padding(2);
            this.txtIntitule.Name = "txtIntitule";
            this.txtIntitule.Size = new System.Drawing.Size(333, 25);
            this.txtIntitule.TabIndex = 27;
            // 
            // btnRefreshD
            // 
            this.btnRefreshD.FlatAppearance.BorderSize = 0;
            this.btnRefreshD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshD.Image = global::PrintServiceCardApp.Properties.Resources.Command_Reset;
            this.btnRefreshD.Location = new System.Drawing.Point(304, 29);
            this.btnRefreshD.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefreshD.Name = "btnRefreshD";
            this.btnRefreshD.Size = new System.Drawing.Size(34, 33);
            this.btnRefreshD.TabIndex = 26;
            this.btnRefreshD.UseVisualStyleBackColor = true;
            this.btnRefreshD.Click += new System.EventHandler(this.btnRefreshD_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::PrintServiceCardApp.Properties.Resources.Command_Reset;
            this.btnRefresh.Location = new System.Drawing.Point(818, 11);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 28);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FonctionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlZone);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.txtResearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstViewData);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FonctionView";
            this.Size = new System.Drawing.Size(874, 689);
            this.Load += new System.EventHandler(this.FonctionView_Load);
            this.pnlZone.ResumeLayout(false);
            this.pnlZone.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstViewData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtResearch;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbGrade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefreshD;
        private System.Windows.Forms.Panel pnlZone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIntitule;
    }
}