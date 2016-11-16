using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartH2O_DU
{
    class SensorsParameter
    {
        public String name { get; set; }
        public String value { get; set; }
        public DateTime date { get; set; }

        public SensorsParameter(String name,String value,DateTime date)
        {
            this.name = name;
            this.value = value;
            this.date = date;
        }
    }
}
