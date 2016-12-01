using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartH2O_DLog
{
    class Program
    {


        private static HandlerXml handlerXml = new SmartH2O_DLog.HandlerXml("param-data.xml");

  
        private static MqttClient m_cClient = new MqttClient("127.0.0.1");
        private static string[] m_strTopicsInfo = { "parameters" };
        private static byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };


        static void Main(string[] args)
        {
            subscriveParameter();
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            handlerXml.putInRealXml(Encoding.UTF8.GetString(e.Message));
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
