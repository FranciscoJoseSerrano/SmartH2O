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



        public void createParameter(String message)
        {
            string[] splited;
            splited = message.Split(';');
            parameter = new SensorsParameter(splited[0],splited[1], splited[2]);
            createXmlDocument();

        }




        private String createXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            /** CREATE XML DECLARATION **/
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(decl);

            XmlElement root = doc.CreateElement("H2O");
            root.SetAttribute("date", Convert.ToString(DateTime.Now));

            doc.AppendChild(root);

            XmlElement param = createSensorParameter(parameter.id, parameter.name, parameter.value,doc);

            root.AppendChild(param);

            Console.WriteLine(doc.OuterXml);

            return doc.OuterXml;

        }

        private XmlElement createSensorParameter(String id , String name, String value, XmlDocument doc)
        {
            XmlElement parameter = doc.CreateElement("parameter");
            parameter.SetAttribute("id", id);

            XmlElement parameterName = doc.CreateElement("name");
            parameterName.InnerText = name;

            XmlElement parameterValue = doc.CreateElement("value");
            parameterValue.InnerText = value;

            parameter.AppendChild(parameterName);
            parameter.AppendChild(parameterValue);


            return parameter;
        }


    }
}
