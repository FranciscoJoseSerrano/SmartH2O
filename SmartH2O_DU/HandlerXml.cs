using System;
using System.Xml;
using System.Xml.Schema;

namespace SmartH2O_DU
{
    class HandlerXml
    {

        private SensorsParameter parameter;
        private string parametersXSDPath = AppDomain.CurrentDomain.BaseDirectory + "parameters.xsd";
        private string ValidationMessage { get; set; }
        private Boolean isValid;

        public HandlerXml()
        {

        }

        public String createParameter(String message)
        {
            string[] splited;
            splited = message.Split(';');
            parameter = new SensorsParameter(splited[0], splited[1], splited[2]);
            String xml = createXmlDocument();
            if (isXMLValid(xml))
            {
                return xml;
            }
            return "";



        }

        private bool isXMLValid(string xml)
        {

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            isValid = true;
            ValidationMessage = "Document Valid";
            try
            {
                ValidationEventHandler eventHandler = new ValidationEventHandler(MyEvent);
                xmlDocument.Schemas.Add(null, parametersXSDPath);
                xmlDocument.Validate(eventHandler);
            }
            catch (XmlException ex)
            {
                isValid = false;
                ValidationMessage = String.Format("Invalid Document {0}", ex.Message);
            }
            return isValid;
        }


        private void MyEvent(object sender, ValidationEventArgs e)
        {
            isValid = false;
            ValidationMessage = "Document is invalid" + e.Message;
        }
    

        private String createXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            /** CREATE XML DECLARATION **/
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(decl);

            XmlElement root = doc.CreateElement("h2o");
            DateTime date = DateTime.Now;
            root.SetAttribute("day", Convert.ToString(date.Day));
            root.SetAttribute("month", Convert.ToString(date.Month));
            root.SetAttribute("year", Convert.ToString(date.Year));
            root.SetAttribute("hour", Convert.ToString(date.Hour));
            root.SetAttribute("minute", Convert.ToString(date.Minute));
            root.SetAttribute("second", Convert.ToString(date.Second));


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
