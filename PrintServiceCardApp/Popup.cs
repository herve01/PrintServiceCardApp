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
    public partial class Popup : Form
    {
        UserControl userControl;
        public Popup(UserControl userControl)
        {
            InitializeComponent();
            this.userControl = userControl;
        }

        public Popup()
        {

        }

        private void Popup_Load(object sender, EventArgs e)
        {
            this.Size = this.userControl.Size;
            this.Width += 20;
            this.Height += 40;
            pnlCtnr.Controls.Clear();
            pnlCtnr.Controls.Add(userControl);
        }

        private void Popup_Resize(object sender, EventArgs e)
        {
            //if (pnlCtnr.Controls != null)
            //{
            //    var view = pnlCtnr.Controls[0] as UserControl;
            //    view.Size = pnlCtnr.Size;
            //}
        }
    }
}
