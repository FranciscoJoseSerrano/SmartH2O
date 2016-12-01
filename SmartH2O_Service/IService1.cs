using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartH2O_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "SmartH20Service" in both code and config file together.
    [ServiceContract]
    public interface SmartH20Service
    {

        [OperationContract]
        List<DatePerHour> GetHourlyInSpecificDay(String year, String month, String day, String parameter);

        // TODO: Add your service operations here
    }

        
      
    [DataContract]
    public class DatePerHour
    {
        [DataMember]
        public string hour { get; set; }
        [DataMember]
        public string average { get; set; }
        [DataMember]
        public string max { get; set; }
        [DataMember]
        public string min { get; set; }
    

        public DatePerHour(string hour, string average, string max, string min)
        {
            this.hour = hour;
            this.average = average;
            this.max = max;
            this.min = min;
        }


    }


   

}
