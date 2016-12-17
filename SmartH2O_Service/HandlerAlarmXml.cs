using System;
using System.IO;
using System.Web;
using System.Xml;

namespace SmartH2O_Service
{
    class HandlerAlarmXml
    {
        private string FilePath { get; set; }

        public HandlerAlarmXml()
        {

            FilePath  = AppDomain.CurrentDomain.BaseDirectory+"/App_Data"+"/"+Properties.Settings.Default.XmlAlarmsPath;

        }

        public void putInAlarmXml(string message)
        {
            XmlDocument doc2;
            //"<parameter name="NH3" alarm_condition="beetween_min" year="2016" month="12" day="8" hour="15" minute="57" second="25"><id>2</id><value>1.75</value><alarm_message>messagem</alarm_message></parameter>"

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
            parameter.SetAttribute("year", a.Attributes["year"].InnerText);
            parameter.SetAttribute("month", a.Attributes["month"].InnerText);
            parameter.SetAttribute("day", a.Attributes["day"].InnerText);
            parameter.SetAttribute("hour", a.Attributes["hour"].InnerText);
            parameter.SetAttribute("minute", a.Attributes["minute"].InnerText);
            parameter.SetAttribute("second", a.Attributes["second"].InnerText);

            XmlElement condition = doc.CreateElement("alarm_condition");
            condition.InnerText = a.Attributes["alarm_condition"].InnerText;

            XmlElement errorMessage = doc.CreateElement("alarm_message");
            errorMessage.InnerText = a["alarm_message"].InnerText;

            XmlElement parameterid = doc.CreateElement("id");
            parameterid.InnerText = a["id"].InnerText;

            XmlElement parameterValue = doc.CreateElement("value");
            parameterValue.InnerText = a["value"].InnerText;

            parameter.AppendChild(parameterid);
            parameter.AppendChild(parameterValue);
            parameter.AppendChild(condition);
            parameter.AppendChild(errorMessage);

            return parameter;
        }
    }
}