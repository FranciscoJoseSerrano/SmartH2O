using System;
using System.IO;
using System.Xml;

namespace SmartH2O_Service
{
    public class FilesWriterService : FilesWriter
    {
        private HandlerAlarmXml handlerAlarmXml = new HandlerAlarmXml();
        private HandlerDataXml handlerDataXml = new HandlerDataXml();

        public void sendWaterAlarm(string message)
        {
            handlerAlarmXml.putInAlarmXml(message);
        }

        public void sendWaterParameter(string message)
        {
            handlerDataXml.putInDataXml(message);
        }
    }
}
