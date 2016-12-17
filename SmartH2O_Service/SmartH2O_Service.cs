using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SmartH2O_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "SmartH20Service" in both code and config file together.
    [ServiceContract]
    public interface SmartH20Service
    {

        [OperationContract]
        String GetHourlyInSpecificDayParameter(String year, String month, String day, String parameter);
        //<?xml version="1.0"?><data><PH><date day="11" month="12" year="2016"><hour>11</hour><average>5,9</average><max>6,8</max><min>5,0</min></date>

        [OperationContract]
        String GetDailyInThresholdParameter(String firstYear, String firstMonth, String firstDay, String secondYear, String secondMonth, String secondDay, String parameter);
        //<?xml version="1.0"?><data><PH><date day="10/12/2016 00:00:00"><average>5,9</average><max>6,8</max><min>5,1</min></date>

        [OperationContract]
        String GetDailyAlarm(String year, String month, String day);//<?xml version ="1.0"?><alarms><date hour="11" minute="46" second="8"><id>3</id><value>8.0</value><alarm_condition>beetween_min</alarm_condition><alarm_message>value of CI2 (10.3) is not less then 10.</alarm_message></date></alarms>

        [OperationContract]
        String GetThresholdAlarm(String firstYear, String firstMonth, String firstDay, String secondYear, String secondMonth, String secondDay);
        //<?xml version="1.0"?><alarms><date day="11" month="12" year="2016"><time hour="11" minute="46" second="8"><id>3</id><value>8.0</value><alarm_condition>beetween_min</alarm_condition><alarm_messag>dadw</alarm_message></time></date></alarms>
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
