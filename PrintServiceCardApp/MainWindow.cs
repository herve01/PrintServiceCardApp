using PrintServiceCardApp.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintServiceCardApp
{
    public partial class MainWindow : Form
    {
        ListeCarteView listeCarteView;
        GradeView gradeView;
        FonctionView fonctionView;
        public MainWindow()
        {
            InitializeComponent();
            listeCarteView = new ListeCarteView();
            gradeView = new GradeView();
            fonctionView = new FonctionView();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            var ctl = ((Button)sender);
            signMenu.Location = new Point(signMenu.Location.X, ctl.Location.Y);
            AddViewIn(listeCarteView);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            AddViewIn(listeCarteView);
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if(pnlCtnrView.Controls !=  null)
            {
                var view = pnlCtnrView.Controls[0] as UserControl;
                view.Size = pnlCtnrView.Size;
            }
            //foreach (var view in pnlCtnrView.Controls.OfType<UserControl>())
            //{
            //    view.Size = pnlCtnrView.Size;
            //}
        }

        void AddViewIn(UserControl view)
        {
            view.Size = pnlCtnrView.Size;
            pnlCtnrView.Controls.Clear();
            pnlCtnrView.Controls.Add(view);
        }

        private void btnGrade_Click(object sender, EventArgs e)
        {
            var ctl = ((Button)sender);
            signMenu.Location = new Point(signMenu.Location.X, ctl.Location.Y);
            AddViewIn(gradeView);
        }

        private void btnFonction_Click(object sender, EventArgs e)
        {
            var ctl = ((Button)sender);
            signMenu.Location = new Point(signMenu.Location.X, ctl.Location.Y);
            AddViewIn(fonctionView);
        }
    }
}
