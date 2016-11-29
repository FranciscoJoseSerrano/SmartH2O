using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartH2O_DLog
{
    class SensorParameterWithDate
    {
        public String second { get; set; }
        public String minute { get; set; }
        public String hour { get; set; }
        public String day { get; set; }
        public String month { get; set; }
        public String year { get; set; }
        public String name { get; set; }
        public String value { get; set; }
        public String id { get; set; }


        public SensorParameterWithDate(String second, String minute, String hour, String day, String month, String year, String id, String name, String value)
        {
            this.second = second;
            this.minute = minute;
            this.hour = hour;
            this.day = day;
            this.month = month;
            this.year = year;
            this.id = id;
            this.name = name;
            this.value = value;
        }
    }
}
