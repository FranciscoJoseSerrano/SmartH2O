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
         

        public List<DatePerHour> GetHourlyInSpecificDay(string year, string month, string day, string parameter)
        {
            List<DatePerHour> listValues;
            listValues = new List<DatePerHour>();
            doc.Load(this.path);
            
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
            return listValues;
        }
        
          
    }
}
