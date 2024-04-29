using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class PersonnelFonction : ModelBase
    {
        public Fonction Fonction { get; set; }
        public Personnel Personnel { get; set; }
        public DateTime Date { get; set; }

        public PersonnelFonction()
        {
            Date = DateTime.Now;
        }
    }
}
