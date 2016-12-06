using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SmartH2O_Service
{

    public class Service1 : SmartH20Service
    {
        private XmlDocument doc = new XmlDocument();
        
        private String path = @"C:\Users\joaos\Desktop\IS_Project\SmartH2O\SmartH2O_DLog\bin\Debug\param-data.xml";

        public String GetDailyInThreshold(string firstYear, string firstMonth, string firstDay, string secondYear, string secondMonth, string secondDay, string parameter)
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
                //DateTime date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
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
            return null;

        }

        public String GetHourlyInSpecificDay(string year, string month, string day, string parameter)
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
                XmlNodeList value = doc.SelectNodes("/data/" + parameter + "/H2O[@day=" + day + "][@month=" + month + "][@year=" + year + "][@hour="+ i +"]/value");
                
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

            }

            return null;
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
                XmlElement element = createElementHourly(datePerHour,date, docSave);
                nameParameter.AppendChild(element);
            }

            return docSave.OuterXml;
            
        }

        private XmlElement createElementDaily(DatePerHour date, DateTime firstDate, DateTime lastDate, XmlDocument docSave)
        {

            XmlElement h2o = docSave.CreateElement("date");
            h2o.InnerText = Convert.ToString(firstDate.Day) + "/" + Convert.ToString(firstDate.Month) +
               "/" + Convert.ToString(firstDate.); //ACABAR ESTA MERDA CARALHO  !!!!!!!!!!!!!



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
