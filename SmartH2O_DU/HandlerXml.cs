using System;
using System.Xml;

namespace SmartH2O_DU
{
    class HandlerXml
    {

        private SensorsParameter parameter;

        public HandlerXml()
        {

        }



        public String createParameter(String message)
        {
            string[] splited;
            splited = message.Split(';');
            parameter = new SensorsParameter(splited[0], splited[1], splited[2]);
            String xml = createXmlDocument();

            return xml;

        }


        private String createXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            /** CREATE XML DECLARATION **/
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(decl);

            XmlElement root = doc.CreateElement("H2O");
            root.SetAttribute("date", Convert.ToString(DateTime.Now));
            root.SetAttribute("type", "DATA");

            doc.AppendChild(root);
            XmlElement param = createSensorParameter(parameter.id, parameter.name, parameter.value, doc);

            root.AppendChild(param);
            

            return doc.OuterXml;

        }


        private XmlElement createSensorParameter(String id, String name, String value, XmlDocument doc)
        {
            XmlElement parameter = doc.CreateElement("parameter");
            parameter.SetAttribute("name", name);

            XmlElement parameterid = doc.CreateElement("id");
            parameterid.InnerText = id;

            XmlElement parameterValue = doc.CreateElement("value");
            parameterValue.InnerText = value;

            parameter.AppendChild(parameterid);
            parameter.AppendChild(parameterValue);


            return parameter;
        }


    }
}
