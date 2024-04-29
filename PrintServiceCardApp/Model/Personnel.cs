using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class Personnel : ModelBase
    {
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Postnom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public SexeType Sexe { get; set; }
        public byte[] Photo { get; set; }
        public byte[] QrCode { get; set; }
        //public string QrCodeStr { get; set; }

        private string _qrCodeStr;

        public string QrCodeStr
        {
            get { 
                return _qrCodeStr; 
            }
            set 
            {
                _qrCodeStr = value;

                if(value != null)
                    QrCode = Helper.ImageUtil.GetCodeQR(value);
            }
        }
        public PersonnelGrade CurrentGrade { get; set; }
        public PersonnelFonction CurrentFonction { get; set; }
        public Affectation Affectation { get; set; }

        public Personnel()
        {
            CurrentGrade = new PersonnelGrade() { Personnel = this };
            CurrentFonction = new PersonnelFonction() { Personnel = this };
            Affectation = new Affectation() { Personnel = this };
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Nom, Postnom, Prenom); 
        }

        public string GradeFonction
        {
            get
            {
                return string.Format("{0}/{1}", CurrentGrade?.Grade?.Id, CurrentFonction?.Fonction?.Intitule);
            }
        }
    }

    public enum SexeType
    {
        Homme,
        Femme
    }
}
