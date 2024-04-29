using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Model
{
    public class ModelBase
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public bool IsSelected { get; set; }
    }
}
