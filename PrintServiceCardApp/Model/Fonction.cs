using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class Fonction : ModelBase
    {
        public string Intitule { get; set; }
        public string Description { get; set; }
        public Grade Grade { get; set; }

        public override string ToString()
        {
            return Intitule;
        }

        public string[] data
        {
            get
            {
                return new string[] { Number.ToString(), Intitule, Description, Grade?.Id ?? "-" };
            }
        }
    }
}
