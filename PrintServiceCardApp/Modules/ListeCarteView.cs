using PrintServiceCardApp.Dao;
using PrintServiceCardApp.Model;
using PrintServiceCardApp.Modules.Reporting.Report;
using PrintServiceCardApp.Modules.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace PrintServiceCardApp.Modules
{
    public partial class ListeCarteView : UserControl
    {
        CardView cardView;
        EditEmployeView editEmployeView;
        ReportView reportView;
        Popup popup;

        List<Personnel> personnels;
        public ListeCarteView()
        {
            InitializeComponent();
            personnels = new List<Personnel>();
            //editEmployeView = new EditEmployeView();
        }

        private void ListeCarteView_Load(object sender, EventArgs e)
        {
            LoadProvince();
            //LoadPersonnel();
        }

        CancellationTokenSource tokenSource = new CancellationTokenSource();

        async Task LoadPersonnel(Personnel personnel = null)
        {
            if(personnel == null)
            {
                var zone = (Zone)cmbZone.SelectedItem;

                if (zone == null)
                    return;

                tokenSource = new CancellationTokenSource();
                var list = await new PersonnelDao().GetAllAsync(zone, tokenSource.Token);

                if(list != null)
                    list.ForEach(p => {
                        cardView = new CardView(p);
                        flowLayoutPanel1.Controls.Add(cardView);
                    });
            }
            else
            {
                cardView = new CardView(personnel);
                flowLayoutPanel1.Controls.Add(cardView);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            editEmployeView = new EditEmployeView();
            popup = new Popup(editEmployeView);
            popup.ShowDialog();

            var pers = editEmployeView?.GetPersonnel;

            if (pers != null & pers.Photo != null)
                LoadPersonnel(pers);
        }

        async Task LoadProvince()
        {
            cmbProvince.Items.Clear();
            cmbProvince.AutoCompleteCustomSource.Clear();

            var dao = new Dao.ProvinceDao();
            dao.NewConnection();

            var list = await Task.Run(() => dao.GetAllAsync());
            cmbProvince.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProvince.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (var row in list)
            {
                //cmbGrade.ValueMember = "Nom";
                cmbProvince.AutoCompleteCustomSource.Add(row.ToString());
                cmbProvince.Items.Add(row);
            }

            cmbProvince.SelectedIndex = list != null ? 0 : -1;
        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            LoadZone();
        }

        void LoadZone()
        {
            cmbZone.Items.Clear();
            cmbZone.AutoCompleteCustomSource.Clear();

            var province = (Province)cmbProvince.SelectedItem;

            if (province == null)
                return;

            cmbZone.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbZone.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (var row in province.Zones)
            {
                //cmbGrade.ValueMember = "Nom";
                cmbZone.AutoCompleteCustomSource.Add(row.ToString());
                cmbZone.Items.Add(row);
            }

            cmbZone.SelectedIndex = province.Zones != null ? 0 : -1;
        }

        private void cmbZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPersonnel();
        }

        async Task print(Zone zone)
        {

            reportView = new ReportView();

            var dao = new PersonnelDao();
            dao.NewConnection();

            var report = new CartesServiceReport();
            var data = await Task.Run(() => dao.GetCarteServiceReportAsync(zone));

            report.SetDataSource(data);
            report.SetParameterValue("chefSignature", AppConfig.ChefSignature);

            reportView.CrystalReportViewer.ReportSource = report;

            popup = new Popup(reportView);
            popup.MinimizeBox = true;
            popup.MaximizeBox = true;
            popup.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var zone = (Zone)cmbZone.SelectedItem;

            if (zone == null)
                return;

            print(zone);
        }
    }
}
