using System;
using System.Xml;

namespace SmartH2O_DU
{
    class HandlerXml
    {


        private SensorsParameter[] parameters;
        private String xmlPath = AppDomain.CurrentDomain.BaseDirectory+"water-parameters.xml";


        public HandlerXml(String[] messages)
        {

            parameters = new SensorsParameter[3];

            createParameters(messages);
        }



        private void createParameters(String[] message)
        {

            string[] splited;
            for (int i = 0; i < message.Length; i++)
            {
                splited = message[i].Split(';');
                parameters[i] = new SensorsParameter(splited[1], splited[2]);

            }

            createXmlDocument();


        }

        private void createXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
       
            /** CREATE XML DECLARATION **/
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(decl);

            XmlElement root = doc.CreateElement("H2O");
            root.SetAttribute("date", Convert.ToString(DateTime.Now));

            doc.AppendChild(root);

            XmlElement ph = createSensorParameter(parameters[0].name, parameters[0].value, doc);
            XmlElement nh3 = createSensorParameter(parameters[1].name, parameters[1].value, doc);
            XmlElement ci2 = createSensorParameter(parameters[2].name, parameters[2].value, doc);

            root.AppendChild(ph);
            root.AppendChild(nh3);
            root.AppendChild(ci2);

            doc.Save(this.xmlPath);




        }

        private XmlElement createSensorParameter(String name,String value,XmlDocument doc)
        {
            XmlElement parameter = doc.CreateElement("parameter");

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
