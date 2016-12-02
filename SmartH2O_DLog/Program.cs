using System;
using System.IO;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace SmartH2O_DLog
{
    class Program
    {
        private static HandlerDataXml handlerDataXml = new SmartH2O_DLog.HandlerDataXml(Properties.Settings.Default.DataFileName);
        private static HandlerAlarmXml handlerAlarmXml = new HandlerAlarmXml(Properties.Settings.Default.AlarmsFileName);

        private static StorageHandler storageHandler = new StorageHandler();
        private static MqttClient m_cClient = new MqttClient("127.0.0.1");
        private static string[] m_strTopicsInfo = { "parameters", "alarms" };
        private static byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };

        static void Main(string[] args)
        {
            subscriveParameter();
            if (!File.Exists(handlerDataXml.XmlFilePath) && storageHandler.existsOnCloud(handlerDataXml.XmlFilePath))
            {
                throw new Exception("Make a copy of the file param-data.xml from storage => no local file!!!");
            }
            if (!File.Exists(handlerAlarmXml.FilePath) && storageHandler.existsOnCloud(handlerAlarmXml.FilePath))
            {
                throw new Exception("Make a copy of the file alarms-data.xml from storage => no local file!!!");
            }
        }



        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.Topic.Contains("alarms"))
            {
                handlerAlarmXml.putInAlarmXml(Encoding.UTF8.GetString(e.Message));
                storageHandler.publishNewInformation(handlerAlarmXml.FilePath);
            }
            else
            {
                handlerDataXml.putInDataXml(Encoding.UTF8.GetString(e.Message));
                storageHandler.publishNewInformation(handlerDataXml.XmlFilePath);
            }
        }

        private static void subscriveParameter()
        {
            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);
        }

    }


}
