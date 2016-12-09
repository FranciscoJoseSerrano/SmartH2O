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
        String GetHourlyInSpecificDay(String year, String month, String day, String parameter);

        [OperationContract]
        String GetDailyInThreshold(String firstYear, String firstMonth, String firstDay, String secondYear, String secondMonth, String secondDay, String parameter);



        // TODO: Add your service operations here
    }

    [ServiceContract]
    public interface FilesWriter
    {
        [OperationContract]
        void sendWaterParameter(string message);

        [OperationContract]
        void sendWaterAlarm(string message);

    }

    [DataContract]
    public class DatePerHour
    {


        [DataMember]
        public string option { get; set; }
        [DataMember]
        public string average { get; set; }
        [DataMember]
        public string max { get; set; }
        [DataMember]
        public string min { get; set; }

        
        public DatePerHour(string option, string average, string max, string min)
        {

            this.option = option;
            this.average = average;
            this.max = max;
            this.min = min;

        }
    }


}
