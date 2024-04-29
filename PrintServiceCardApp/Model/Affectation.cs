using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class Affectation : ModelBase
    {
        public Personnel Personnel { get; set; }
        public Zone Zone { get; set; }
        public DateTime Date { get; set; }

        public Affectation()
        {
            Date = DateTime.Now;
        }
    }
}
