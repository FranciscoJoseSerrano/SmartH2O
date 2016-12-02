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
            //alarm_condition
            //<?xml version=\"1.0\"?><h2o day=\"2\" month=\"12\" year=\"2016\" hour=\"12\" 
            //minute =\"59\" second=\"30\" type=\"data\"><parameter name=\"PH\"><id>1</id><value>5.8</value></parameter></h2o>

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(message);

            XmlNode alarm = doc.SelectSingleNode("/h2o");


            if (!File.Exists(FilePath))
            {
                doc2 = createAlarmFile();
            }
            else
            {
                doc2 = new XmlDocument();
                doc2.LoadXml(FilePath);
            }

            XmlNode lastChild = doc2.LastChild;
            lastChild.AppendChild(alarm);

            doc2.Save(FilePath);
        }

        private XmlDocument createAlarmFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(decl);

            XmlElement root = doc.CreateElement("Alarms");
            doc.AppendChild(root);

            return doc;

        }
    }
}
