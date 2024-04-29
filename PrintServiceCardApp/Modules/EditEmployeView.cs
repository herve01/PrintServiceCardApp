using PrintServiceCardApp.Dao;
using PrintServiceCardApp.Model;
using PrintServiceCardApp.Modules.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace PrintServiceCardApp.Modules
{
    public partial class EditEmployeView : UserControl
    {
        PhotoView photoView;
        Popup popup;
        private Array sexes;
        Personnel personnel;
        bool ForEdding;

        public EditEmployeView()
        {
            personnel = new Personnel();
            InitializeComponent();
            sexes = Enum.GetValues(typeof(SexeType));  
        }
        public EditEmployeView(Personnel personnel) : this()
        {
            this.personnel = personnel;
            ForEdding = true;
        }

        private void ListeCarteView_Load(object sender, EventArgs e)
        {
            LoadSexe();
            LoadGrade();
            LoadProvince();

            if(ForEdding)
            {

                txtNom.Text = personnel.Nom;
                txtPostnom.Text = personnel.Postnom;
                txtPrenom.Text = personnel.Prenom;
                cmbSexe.SelectedItem = personnel.Sexe;
                cmbFonction.SelectedItem = personnel.CurrentFonction.Fonction;
                cmbGrade.SelectedItem = personnel.CurrentGrade.Grade.Id;
                cmbZone.SelectedItem = personnel.Affectation.Zone.ToString();
                txtPhone.Text = personnel.Telephone;
                txtMatricule.Text = personnel.Matricule;
                cmbProvince.SelectedItem = personnel.Affectation.Zone.Province;
                cmbZone.SelectedItem = personnel.Affectation.Zone;

                if (personnel.Photo != null)
                {
                    pbPhoto.Image = Model.Helper.ImageUtil.ByteToBitmap(personnel.Photo);
                }

            }
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            photoView = new PhotoView();
            popup = new Popup(photoView);
            popup.ShowDialog();

            if(photoView.Image != null)
            {
                personnel.Photo = photoView.Image;
                pbPhoto.Image = Model.Helper.ImageUtil.ByteToBitmap(personnel.Photo);
            }
        }

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

        void LoadSexe()
        {
            cmbSexe.Items.Clear();
            cmbSexe.AutoCompleteCustomSource.Clear();

            cmbSexe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbSexe.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (var row in sexes)
            {
                //cmbFaculte.ValueMember = "Nom";
                cmbSexe.AutoCompleteCustomSource.Add(row.ToString());
                cmbSexe.Items.Add(row);
            }

            cmbSexe.SelectedIndex = sexes != null ? 0 : -1;
        }

        async Task LoadFonction()
        {
            cmbFonction.Items.Clear();
            cmbFonction.AutoCompleteCustomSource.Clear();

            var grade = (Grade)cmbGrade.SelectedItem;

            if (grade == null)
                return;

            var dao = new Dao.FonctionDao();
            dao.NewConnection();

            var list = await Task.Run(() => dao.GetAllAsync(grade));

            cmbFonction.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFonction.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (var row in list)
            {
                //cmbGrade.ValueMember = "Nom";
                cmbFonction.AutoCompleteCustomSource.Add(row.ToString());
                cmbFonction.Items.Add(row);
            }

            cmbFonction.SelectedIndex = list != null ? 0 : -1;
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

        private void cmbGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFonction();
        }

        public Personnel GetPersonnel
        {
            get => personnel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Functions.IsEmptyTextBox(this))
                MessageBox.Show("Une Erreur est survenue lors de l'enregistrement.\n Rassurez-vous d'avoir rempli tous les champs !!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Edit();
            }
        }

        async Task Edit()
        {
            personnel.Nom = txtNom.Text;
            personnel.Postnom = txtPostnom.Text;
            personnel.Prenom = txtPrenom.Text;
            personnel.Sexe = (SexeType)cmbSexe.SelectedItem;
            personnel.CurrentFonction.Fonction = (Fonction)cmbFonction.SelectedItem;
            personnel.CurrentGrade.Grade = (Grade)cmbGrade.SelectedItem;
            personnel.Affectation.Zone = (Zone)cmbZone.SelectedItem;
            personnel.Telephone = txtPhone.Text;
            personnel.Matricule = txtMatricule.Text;

            var numero = new PersonnelDao().GetCurrentNumero();
            var _code = numero == null ? "0" : numero.Split('/')[0]?.Split('.')[1];

            var count = int.Parse(_code) + 1;

            personnel.QrCodeStr = string.Format("S.{0}/1428/{1}", count.ToString("D3"), DateTime.Now.Year);

            if (await new Dao.PersonnelDao().AddAsync(personnel) > 0)
            {
                MessageBox.Show("Enregistrement reussi avec succès !!", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Functions.InitTextBox(this);

                Task.Delay(1000).Wait(1000);

                ((Form)this.TopLevelControl).Close();
            }
        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadZone();
        }
    }
}
