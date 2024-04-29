using PrintServiceCardApp.Extension;
using PrintServiceCardApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintServiceCardApp.Modules
{
    public partial class FonctionView : UserControl
    {
        Fonction fonction;
        List<Fonction> fonctions;

        public FonctionView()
        {
            InitializeComponent();
            fonctions = new List<Fonction>();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Functions.IsEmptyTextBox(pnlZone))
                MessageBox.Show("Une Erreur est survenue lors de l'enregistrement.\n Rassurez-vous d'avoir rempli tous les champs !!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                fonction = new Fonction();
                fonction.Intitule = txtIntitule.Text;
                fonction.Description = txtDescription.Text;
                fonction.Grade = (Grade)cmbGrade.SelectedItem;

                if (new Dao.FonctionDao().Add(fonction) > 0)
                {
                    MessageBox.Show("Enregistrement reussi avec succès !!", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Add(fonction);
                    Functions.InitTextBox(pnlZone);
                }
                else
                {
                    MessageBox.Show("Une Erreur est survenue lors de l'enregistrement !!", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        async Task LoadGrade()
        {
            cmbGrade.Items.Clear();
            cmbGrade.AutoCompleteCustomSource.Clear();

            var dao = new Dao.GradeDao();
            dao.NewConnection();

            var list = await Task.Run(() => dao.GetAllAsync());

            cmbGrade.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbGrade.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (var row in list)
            {
                //cmbGrade.ValueMember = "Nom";
                cmbGrade.AutoCompleteCustomSource.Add(row.ToString());
                cmbGrade.Items.Add(row);
            }

            cmbGrade.SelectedIndex = list != null ? 0 : -1;
        }

        async Task LoadFonction()
        {
            tokenSource = new CancellationTokenSource();

            lstViewData.Items.Clear();

            fonctions = await Task.Run(() => new Dao.FonctionDao().GetAllAsync(tokenSource.Token));

            lblCount.Text = string.Format("({0})", fonctions.Count);

            Add();
        }

        void Add(Fonction instance = null)
        {
            var list = new List<Grade>();

            if (instance == null)
                foreach (var row in fonctions)
                {
                    lstViewData.Items.Add(new ListViewItem(row.data));
                }
            else
            {
                instance.Number = fonctions.Count == 0 ? 1 : fonctions.FindLast(s => s.Number > 0).Number + 1;
                lstViewData.Items.Add(new ListViewItem(instance.data));

                fonctions.Add(instance);
                lblCount.Text = string.Format("({0})", fonctions.Count);
            }
        }

        void DrawListView()
        {
            lstViewData.View = System.Windows.Forms.View.Details;
            lstViewData.GridLines = true;
            lstViewData.FullRowSelect = true;

            var sizeColumn = (lstViewData.Width - 150) / 2;

            //Add column header
            lstViewData.Columns.Add("#", 50);
            lstViewData.Columns.Add("Intitule", sizeColumn - 50);
            lstViewData.Columns.Add("Description", sizeColumn + 50);
            lstViewData.Columns.Add("Grade", 100);
        }

        private void FonctionView_Load(object sender, EventArgs e)
        {
           
            DrawListView();
            LoadGrade();

            LoadFonction();
            
        }

        private void btnRefreshD_Click(object sender, EventArgs e)
        {
            LoadGrade();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFonction();
        }

        private void txtResearch_TextChanged(object sender, EventArgs e)
        {
            var motif = ((TextBox)sender).Text.Trim().ToLower().NoAccent();
            if (motif == null)
                return;

            lstViewData.Items.Clear();

            lstViewData.Items.AddRange(fonctions.Where(i => string.IsNullOrEmpty(motif) 
            || i.Intitule.ToLower().Trim().NoAccent().StartsWith(motif) 
            || i.Intitule.ToLower().Trim().NoAccent().Contains(motif)
            || i.Grade.Id.ToLower().Trim().NoAccent().StartsWith(motif)
            ).Select(c => new ListViewItem(c.data)).ToArray());
        }
    }
}
