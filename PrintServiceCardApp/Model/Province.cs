using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class Province : ModelBase
    {
        public new int Id { get; set; }
        public string Nom { get; set; }
        public List<Zone> Zones { get; set; }

        public Province()
        {
            Zones = new List<Zone>();
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
