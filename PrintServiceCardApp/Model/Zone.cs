using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class Zone : ModelBase
    {
        public new int Id { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }

        public Province Province { get; set; }

        public override string ToString()
        {
            return Nom;
        }
    }
}
