using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace SmartH2O_DLog
{
    class Program
    {
        private static HandlerDataXml handlerDataXml = new SmartH2O_DLog.HandlerDataXml("param-data.xml");
        private static HandlerAlarmXml handlerAlarm = new HandlerAlarmXml("alarms-data.xml");

        private static StorageHandler storageHandler = new StorageHandler();
        private static MqttClient m_cClient = new MqttClient("192.168.1.71");
        private static string[] m_strTopicsInfo = { "parameters", "alarms" };
        private static byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };

        static void Main(string[] args)
        {
            subscriveParameter();
        }



        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.Topic.Contains("alarms"))
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Message));
                return;
            }
            handlerDataXml.putInDataXml(Encoding.UTF8.GetString(e.Message));
            storageHandler.publishNewInformation();

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
