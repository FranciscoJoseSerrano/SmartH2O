using System;
using System.Collections.Generic;
using System.Xml;

namespace SmartH2O_Service
{

    public class UserService : SmartH20Service
    {
        private XmlDocument doc = new XmlDocument();

        private String path = @"C:\Users\joaos\Desktop\real_is\SmartH2O\SmartH2O_Service\App_Data\param-data.xml";
        private String pathAlarm = @"C:\Users\joaos\Desktop\real_is\SmartH2O\SmartH2O_Service\App_Data\alarms-data.xml";

        public string GetDailyAlarm(string year, string month, string day)
        {

            doc.Load(this.pathAlarm);

            XmlNodeList value = doc.SelectNodes("/alarms/parameter[@day=" + day + "][@month=" + month + "][@year=" + year + "]");


            XmlDocument docSave = new XmlDocument();

            XmlDeclaration dec = docSave.CreateXmlDeclaration("1.0", null, null);
            docSave.AppendChild(dec);

            XmlElement root = docSave.CreateElement("alarms");

            docSave.AppendChild(root);

    

            foreach (XmlNode item in value)
            {

                XmlElement hour = docSave.CreateElement("date");
                hour.SetAttribute("hour", item.Attributes["hour"].InnerText);
                hour.SetAttribute("minute", item.Attributes["minute"].InnerText);
                hour.SetAttribute("second", item.Attributes["second"].InnerText);

                XmlElement id = docSave.CreateElement("id");
                id.InnerText = item["id"].InnerText;

                XmlElement valor = docSave.CreateElement("value");
                valor.InnerText = item["value"].InnerText;

                XmlElement alarmCondition = docSave.CreateElement("alarm_condition");
                alarmCondition.InnerText = item["alarm_condition"].InnerText;

                root.AppendChild(hour);
                hour.AppendChild(id);
                id.AppendChild(valor);
                valor.AppendChild(alarmCondition);

              
            }

            return docSave.OuterXml;

        }




        public string GetThresholdAlarm(string firstYear, string firstMonth, string firstDay, string secondYear, string secondMonth, string secondDay)
        {
            doc.Load(this.pathAlarm);
            DateTime firstDate = new DateTime(int.Parse(firstYear), int.Parse(firstMonth), int.Parse(firstDay));
            DateTime secondDate = new DateTime(int.Parse(secondYear), int.Parse(secondMonth), int.Parse(secondDay));

            XmlDocument docSave = new XmlDocument();

            XmlDeclaration dec = docSave.CreateXmlDeclaration("1.0", null, null);
            docSave.AppendChild(dec);

            XmlElement root = docSave.CreateElement("alarms");
            docSave.AppendChild(root);



            for (DateTime date = firstDate; date <= secondDate; date = date.AddDays(1.0))
            {
                XmlNodeList value = doc.SelectNodes("/alarms/parameter[@day="+ date.Day + "][@month=" + date.Month +"][@year="+ date.Year+"]");

                XmlElement dates = docSave.CreateElement("date");
                dates.SetAttribute("day", Convert.ToString(date.Day));
                dates.SetAttribute("month", Convert.ToString(date.Month));
                dates.SetAttribute("year", Convert.ToString(date.Year));

                root.AppendChild(dates);

                foreach (XmlNode item in value)
                {
                    XmlElement time = docSave.CreateElement("time");
                    time.SetAttribute("hour", item.Attributes["hour"].InnerText);
                    time.SetAttribute("minute", item.Attributes["minute"].InnerText);
                    time.SetAttribute("second", item.Attributes["second"].InnerText);

                    XmlElement id = docSave.CreateElement("id");
                    id.InnerText = item["id"].InnerText;

                    XmlElement valor = docSave.CreateElement("value");
                    valor.InnerText = item["value"].InnerText;

                    XmlElement alarmCondition = docSave.CreateElement("alarm_condition");
                    alarmCondition.InnerText = item["alarm_condition"].InnerText;

                    dates.AppendChild(time);
                    time.AppendChild(id);
                    id.AppendChild(valor);
                    valor.AppendChild(alarmCondition);
                }              
                
            }

            return docSave.OuterXml;
        }
        


        public String GetDailyInThresholdParameter(string firstYear, string firstMonth, string firstDay, string secondYear, string secondMonth, string secondDay, string parameter)
        {
            List<DatePerHour> listValues;
            listValues = new List<DatePerHour>();
            doc.Load(this.path);

            DateTime firstDate = new DateTime(int.Parse(firstYear), int.Parse(firstMonth), int.Parse(firstDay));
            DateTime secondDate = new DateTime(int.Parse(secondYear), int.Parse(secondMonth), int.Parse(secondDay));

            for (DateTime date = firstDate; date <= secondDate; date = date.AddDays(1.0))
            {
                float sum = 0;
                float max = 0;
                float min = 100;
                int number = 0;
                double average = 0.0;
                XmlNodeList value = doc.SelectNodes("/data/" + parameter + "/H2O[@day=" + date.Day + "][@month=" + date.Month + "][@year=" + date.Year + "]/value");
                
                if (value.Count != 0)
                {

                    foreach (XmlNode item in value)
                    {
                        float trueValue = float.Parse(item.InnerText.Replace(".", ","));

                        if (trueValue >= max)
                        {
                            max = trueValue;
                        }

                        if (trueValue <= min)
                        {
                            min = trueValue;
                        }

                        sum += trueValue;
                        number++;
                    }

                    average = (double)sum / number;

                    listValues.Add(new DatePerHour(date.ToString(), average.ToString("0.0"), max.ToString("0.0"), min.ToString("0.0")));


                }

            }
            string response = ReceiveFromDailyXML(listValues, parameter, firstDate, secondDate);
            return response;

        }

        public String GetHourlyInSpecificDayParameter(string year, string month, string day, string parameter)
        {

            List<DatePerHour> listValues;
            listValues = new List<DatePerHour>();
            doc.Load(this.path);
            DateTime date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            for (int i = 0; i < 24; i++)
            {
                float sum = 0;
                float max = 0;
                float min = 100;
                int number = 0;
                double average = 0.0;
                XmlNodeList value = doc.SelectNodes("/data/" + parameter + "/H2O[@day=" + day + "][@month=" + month + "][@year=" + year + "][@hour=" + i + "]/value");

                if (value.Count != 0)
                {

                    foreach (XmlNode item in value)
                    {
                        float trueValue = float.Parse(item.InnerText.Replace(".", ","));

                        if (trueValue >= max)
                        {
                            max = trueValue;
                        }

                        if (trueValue <= min)
                        {
                            min = trueValue;
                        }

                        sum += trueValue;
                        number++;
                    }

                    average = (double)sum / number;

                    listValues.Add(new DatePerHour((Convert.ToString(i)), average.ToString("0.0"), max.ToString("0.0"), min.ToString("0.0")));
                }
            }

            string s = ReceiveFromHourlyXML(listValues, date, parameter);

            return s;
        }

        

        private string ReceiveFromDailyXML(List<DatePerHour> daily, string parameter, DateTime firstDate, DateTime lastDate)
        {
            XmlDocument docSave = new XmlDocument();

            XmlDeclaration dec = docSave.CreateXmlDeclaration("1.0", null, null);
            docSave.AppendChild(dec);

            XmlElement root = docSave.CreateElement("data");
            XmlElement nameParameter = docSave.CreateElement(parameter);



            docSave.AppendChild(root);
            root.AppendChild(nameParameter);

            foreach (DatePerHour datePerHour in daily)
            {
                XmlElement element = createElementDaily(datePerHour, firstDate, lastDate, docSave);
                nameParameter.AppendChild(element);
            }

            return docSave.OuterXml;
        }

        private string ReceiveFromHourlyXML(List<DatePerHour> hourly, DateTime date, string parameter)
        {
            XmlDocument docSave = new XmlDocument();

            //criar o conteudo do documento
            XmlDeclaration dec = docSave.CreateXmlDeclaration("1.0", null, null);
            docSave.AppendChild(dec);

            XmlElement root = docSave.CreateElement("data");
            XmlElement nameParameter = docSave.CreateElement(parameter);



            docSave.AppendChild(root);
            root.AppendChild(nameParameter);


            foreach (DatePerHour datePerHour in hourly)
            {
                XmlElement element = createElementHourly(datePerHour, date, docSave);
                nameParameter.AppendChild(element);
            }

            return docSave.OuterXml;

        }

       


        private XmlElement createElementDaily(DatePerHour date, DateTime firstDate, DateTime lastDate, XmlDocument docSave)
        {

            XmlElement first = docSave.CreateElement("since");
            first.SetAttribute("day", Convert.ToString(firstDate.Day));
            first.SetAttribute("month", Convert.ToString(firstDate.Month));
            first.SetAttribute("year", Convert.ToString(firstDate.Year));

            XmlElement second = docSave.CreateElement("until");
            second.SetAttribute("day", Convert.ToString(lastDate.Day));
            second.SetAttribute("month", Convert.ToString(lastDate.Month));
            second.SetAttribute("year", Convert.ToString(lastDate.Year));


            XmlElement hour = docSave.CreateElement("day");
            hour.InnerText = date.option;

            XmlElement average = docSave.CreateElement("average");
            average.InnerText = date.average;

            XmlElement max = docSave.CreateElement("max");
            max.InnerText = date.max;

            XmlElement min = docSave.CreateElement("min");
            min.InnerText = date.min;

            first.AppendChild(second);
            second.AppendChild(hour);
            hour.AppendChild(average);
            average.AppendChild(max);
            max.AppendChild(min);

            return first;
        }


        private XmlElement createElementHourly(DatePerHour date, DateTime dateTime, XmlDocument docSave)
        {

            XmlElement h2o = docSave.CreateElement("date");
            h2o.SetAttribute("day", Convert.ToString(dateTime.Day));
            h2o.SetAttribute("month", Convert.ToString(dateTime.Month));
            h2o.SetAttribute("year", Convert.ToString(dateTime.Year));

            XmlElement hour = docSave.CreateElement("hour");
            hour.InnerText = date.option;

            XmlElement average = docSave.CreateElement("average");
            average.InnerText = date.average;

            XmlElement max = docSave.CreateElement("max");
            max.InnerText = date.max;

            XmlElement min = docSave.CreateElement("min");
            min.InnerText = date.min;

            h2o.AppendChild(hour);
            hour.AppendChild(average);
            average.AppendChild(max);
            max.AppendChild(min);

            return h2o;
        }

    
    }
}
