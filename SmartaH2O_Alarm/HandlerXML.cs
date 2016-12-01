using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace SmartaH2O_Alarm
{
    class HandlerXML
    {
        public string xmlFileRulesPath = AppDomain.CurrentDomain.BaseDirectory+"trigger_rules.xml";
        public string xsdFileRulesPath = AppDomain.CurrentDomain.BaseDirectory + "trigger_rules.xsd";

        public string ValidationMessage { get; private set; }

        public Boolean isXMLValid(string XMLpath, string XSDpath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(XMLpath);
                ValidationEventHandler validationHandler = new ValidationEventHandler(MyEvent);
                doc.Schemas.Add(null, XSDpath);
                doc.Validate(validationHandler);
            }
            catch (XmlException exception)
            {
             ValidationMessage = String.Format("Invalid Document {0}", exception.Message);
                return false;
            }

            return true;
        }

            private void MyEvent(object sender, ValidationEventArgs e)
        {
            ValidationMessage = "Document is invalid" + e.Message;
        }

        private void readXML(string XMLpath, string XSDpath)
        {
            if(isXMLValid(XMLpath, XSDpath))
            {

            }
        }

    }

   
}
