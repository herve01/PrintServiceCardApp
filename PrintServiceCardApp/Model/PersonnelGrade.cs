using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class PersonnelGrade : ModelBase
    {
        public Grade Grade { get; set; }
        public Personnel Personnel { get; set; }
        public DateTime Date { get; set; }

        public PersonnelGrade()
        {
            Date = DateTime.Now;
        }
    }
}
