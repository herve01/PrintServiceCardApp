using PrintServiceCardApp.Dao;
using PrintServiceCardApp.Model;
using PrintServiceCardApp.Modules.Reporting.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace PrintServiceCardApp.Modules.Template
{
    public partial class CardView : UserControl
    {
        Personnel personnel;
        ReportView reportView;
        EditEmployeView editEmployeView;
        Popup popup;
        public CardView(Personnel personnel)
        {
            InitializeComponent();
            this.personnel = personnel;
        }

        private void CardView_Load(object sender, EventArgs e)
        {
            lblName.Text = personnel.ToString();
            lblGdFct.Text = personnel.GradeFonction;
            lblMatricule.Text = personnel.Matricule;
            lblAffectation.Text = personnel?.Affectation?.Zone.Nom;
            lblCodeQR.Text = personnel.QrCodeStr;
            lbldate.Text = DateTime.Now.ToString("dd.MM.yyyy");
            lblSexe.Text = personnel.Sexe.ToString();
            lblMinist.Text = AppConfig.ChefSignature;

            if (personnel.Photo != null && personnel.QrCode != null)
            {
                pbPhoto.Image = Functions.ByteToImage(personnel.Photo);
                pbQR.Image = Functions.ByteToImage(personnel.QrCode);
            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //editEmployeView = new EditEmployeView(personnel);
            //popup = new Popup(editEmployeView);
            //popup.ShowDialog();
        }

        private void CardView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, new Point(e.X, e.Y));
            }
        }


        async Task print(Personnel personnel)
        {

            reportView = new ReportView();

            var dao = new PersonnelDao();
            dao.NewConnection();

            var report = new CarteServiceReport();
            var data = await Task.Run(() => dao.GetCarteServiceReportAsync(personnel));

            report.SetDataSource(data);
            report.SetParameterValue("chefSignature", AppConfig.ChefSignature);

            reportView.CrystalReportViewer.ReportSource = report;

            popup = new Popup(reportView);
            popup.MinimizeBox = true;
            popup.MaximizeBox = true;
            popup.ShowDialog();
        }

        private void imprimerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (personnel == null)
                return;

            print(personnel);
        }
    }
}
