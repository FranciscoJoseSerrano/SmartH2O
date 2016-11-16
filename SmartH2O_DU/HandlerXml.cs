using System;
using System.Xml;

namespace SmartH2O_DU
{
    class HandlerXml
    {


        private SensorsParameter[] parameters;
        private String xmlPath = AppDomain.CurrentDomain.BaseDirectory;


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

                parameters[i] = new SensorsParameter(splited[1], splited[2],DateTime.Now);

            }


        }

        private void createXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            /** CREATE XML DECLARATION **/
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(decl);


        }


    }
}
