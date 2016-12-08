using System.IO;
using System.Xml;

namespace SmartH2O_DLog
{

    class HandlerAlarmXml
    {
        public string FilePath { get; set; }

        public HandlerAlarmXml(string filepath)
        {
            FilePath = filepath;
        }

        public void putInAlarmXml(string message)
        {
            XmlDocument doc2;
            //"<parameter name="NH3" alarm_condition="beetween_min"><id>2</id><value>1.24</value></parameter>"


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(message);

            XmlNode alarmNode = doc.SelectSingleNode("/parameter");



            if (!File.Exists(FilePath))
            {
                doc2 = createAlarmFile();
            }
            else
            {
                doc2 = new XmlDocument();
                doc2.Load(FilePath);
            }

            XmlElement alarm = createAlarmElement(alarmNode, doc2);
            doc2.LastChild.AppendChild(alarm);





            doc2.Save(FilePath);
        }

        private XmlDocument createAlarmFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(decl);

            XmlElement root = doc.CreateElement("alarms");
            doc.AppendChild(root);

            return doc;

        }

        private XmlElement createAlarmElement(XmlNode a, XmlDocument doc)
        {

            XmlElement parameter = doc.CreateElement("parameter");
            parameter.SetAttribute("name", a.Attributes["name"].InnerText);
            parameter.SetAttribute("alarm_condition", a.Attributes["alarm_condition"].InnerText);


            XmlElement parameterid = doc.CreateElement("id");
            parameterid.InnerText = a["id"].InnerText;

            XmlElement parameterValue = doc.CreateElement("value");
            parameterValue.InnerText = a["value"].InnerText;

            parameter.AppendChild(parameterid);
            parameter.AppendChild(parameterValue);


            return parameter;
        }
    }
}
