using System;
using System.Xml;

namespace SmartH2O_DLog
{
    class HandlerDataXml
    {

        public string XmlFilePath { get; set; }



        public HandlerDataXml(string xmlFile)
        {
            this.XmlFilePath = xmlFile;
        }

        public void putInDataXml(String message)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(message);
            XmlNode root = doc.SelectSingleNode("/h2o");
            XmlNode parameter = doc.SelectSingleNode("/h2o/parameter");
            string second = root.Attributes["second"].InnerText;
            string minute = root.Attributes["minute"].InnerText;
            string hour = root.Attributes["hour"].InnerText;
            string day = root.Attributes["day"].InnerText;
            string month = root.Attributes["month"].InnerText;
            string year = root.Attributes["year"].InnerText;
            string name = parameter.Attributes["name"].InnerText;
            string id = parameter["id"].InnerText;
            string value = parameter["value"].InnerText;
            SensorParameterWithDate realParameter = new SensorParameterWithDate(second, minute, hour, day, month, year, id, name, value);

            if (System.IO.File.Exists(this.XmlFilePath) == false)
            {
                creatDataXml(realParameter);
            }
            else
            {
                addParameter(realParameter);
            }
        }

        public void creatDataXml(SensorParameterWithDate sensorParameterWithDate)
        {

            XmlDocument doc = new XmlDocument();

            //criar o conteudo do documento
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);

            XmlElement root = doc.CreateElement("data");
            XmlElement parameter = doc.CreateElement(sensorParameterWithDate.name);
            parameter.SetAttribute("id", sensorParameterWithDate.id);

            XmlElement b = this.createSensorWithDateParameter(sensorParameterWithDate.second, sensorParameterWithDate.minute, sensorParameterWithDate.hour, sensorParameterWithDate.day,
                sensorParameterWithDate.month, sensorParameterWithDate.year, sensorParameterWithDate.id, sensorParameterWithDate.name, sensorParameterWithDate.value, doc);

            doc.AppendChild(root);

            root.AppendChild(parameter);
            parameter.AppendChild(b);

            doc.Save(this.XmlFilePath);

        }

        private XmlElement createSensorWithDateParameter(String second, String minute, String hour, String day, String month, String year, String id, String name, String value, XmlDocument doc)
        {

            XmlElement h2o = doc.CreateElement("H2O");
            h2o.SetAttribute("day", day);
            h2o.SetAttribute("month", month);
            h2o.SetAttribute("year", year);
            h2o.SetAttribute("hour", hour);
            h2o.SetAttribute("minute", minute);
            h2o.SetAttribute("second", second);


            XmlElement parameterValue = doc.CreateElement("value");
            parameterValue.InnerText = value;

            h2o.AppendChild(parameterValue);

            return h2o;


        }

        private void addParameter(SensorParameterWithDate sensorParameterWithDate)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.XmlFilePath);

            XmlElement b = this.createSensorWithDateParameter(sensorParameterWithDate.second, sensorParameterWithDate.minute,
                sensorParameterWithDate.hour, sensorParameterWithDate.day, sensorParameterWithDate.month, sensorParameterWithDate.year,
                sensorParameterWithDate.id, sensorParameterWithDate.name, sensorParameterWithDate.value, doc);

            if (verifyParameter(sensorParameterWithDate.id) == false)
            {

                XmlElement parameter = doc.CreateElement(sensorParameterWithDate.name);
                parameter.SetAttribute("id", sensorParameterWithDate.id);

                XmlNode last = doc.LastChild;
                last.AppendChild(parameter);
                parameter.AppendChild(b);
            }
            else
            {
                XmlNode nodeParam = doc.SelectSingleNode("data/" + sensorParameterWithDate.name);

                nodeParam.AppendChild(b);

            }

            doc.Save(this.XmlFilePath);
        }


        private bool verifyParameter(string id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.XmlFilePath);

            XmlNodeList parameters = doc.SelectNodes("data//@id");

            foreach (XmlNode node in parameters)
            {


                if (node.Value == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
