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
    public partial class GradeView : UserControl
    {
        Grade grade;
        List<Grade> grades;
        public string[] types { get; set; }

        public GradeView()
        {
            InitializeComponent();
            grades = new List<Grade>();
            types = new string[] { "Agent", "Cadre", "Haut-cadre" };

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Functions.IsEmptyTextBox(pnlZone))
                MessageBox.Show("Une Erreur est survenue lors de l'enregistrement.\n Rassurez-vous d'avoir rempli tous les champs !!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                grade = new Grade();
                grade.Intitule = txtIntitule.Text;
                grade.Description = txtDescription.Text;
                grade.Niveau =  int.Parse(txtNiveau.Text);
                grade.Type = (string)cmbType.SelectedItem;

                if (new Dao.GradeDao().Add(grade) > 0)
                {
                    MessageBox.Show("Enregistrement reussi avec succès !!", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Add(grade);
                    Functions.InitTextBox(pnlZone);
                }
            }
        }
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        void LoadType()
        {
            cmbType.Items.Clear();
            cmbType.AutoCompleteCustomSource.Clear();

            cmbType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbType.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (var row in types)
            {
                //cmbFaculte.ValueMember = "Nom";
                cmbType.AutoCompleteCustomSource.Add(row.ToString());
                cmbType.Items.Add(row);
            }

            cmbType.SelectedIndex = types != null ? 0 : -1;
        }

        async Task LoadGrade()
        {
            tokenSource = new CancellationTokenSource();

            lstViewData.Items.Clear();

            grades = await Task.Run(() => new Dao.GradeDao().GetAllAsync(tokenSource.Token));

            lblCount.Text = string.Format("({0})", grades.Count);

            Add();
        }

        void Add(Grade instance = null)
        {
            var list = new List<Grade>();

            if (instance == null)
                foreach (var row in grades)
                {
                    lstViewData.Items.Add(new ListViewItem(row.data));
                }
            else
            {
                instance.Number = grades.Count == 0 ? 1 : grades.FindLast(s => s.Number > 0).Number + 1;
                lstViewData.Items.Add(new ListViewItem(instance.data));

                grades.Add(instance);
                lblCount.Text = string.Format("({0})", grades.Count);
            }
        }

        void DrawListView()
        {
            lstViewData.View = System.Windows.Forms.View.Details;
            lstViewData.GridLines = true;
            lstViewData.FullRowSelect = true;

            var sizeColumn = (lstViewData.Width - 200) / 2;

            ////Add column header
            lstViewData.Columns.Add("#", 50);
            lstViewData.Columns.Add("Intitule", sizeColumn - 60);
            lstViewData.Columns.Add("Type", 150);
            lstViewData.Columns.Add("Description", sizeColumn + 70);
        }

        private void GradeView_Load(object sender, EventArgs e)
        {
            DrawListView();
            LoadType();

            LoadGrade();
        }

        private void btnRefreshD_Click(object sender, EventArgs e)
        {
            //LoadFaculte();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrade();
        }

        private void txtResearch_TextChanged(object sender, EventArgs e)
        {
            var motif = ((TextBox)sender).Text.Trim().ToLower().NoAccent();
            if (motif == null)
                return;

            lstViewData.Items.Clear();

            lstViewData.Items.AddRange(grades.Where(i => string.IsNullOrEmpty(motif) || i.Intitule.ToLower().Trim().NoAccent().StartsWith(motif) || i.Intitule.ToLower().Trim().NoAccent().Contains(motif))
                .Select(c => new ListViewItem(c.data)).ToArray());
        }
    }
}
