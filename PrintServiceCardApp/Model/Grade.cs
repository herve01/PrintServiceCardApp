using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class Grade : ModelBase
    {
        public string Intitule { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Niveau { get; set; }

        public override string ToString()
        {
            return Id; 
        }

        public string[] data
        {
            get
            {
                return new string[] { Number.ToString(), Intitule, Type, Description };
            }
        }
    }
}
