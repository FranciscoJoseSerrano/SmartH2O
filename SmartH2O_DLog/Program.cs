using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace SmartH2O_DLog
{
    class Program
    {
        private static FilesWriterService.FilesWriterClient client = new FilesWriterService.FilesWriterClient();
        private static MqttClient m_cClient = new MqttClient("127.0.0.1");
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
                var task = Task.Run(() => client.sendWaterAlarmAsync(Encoding.UTF8.GetString(e.Message)));
            }
            else
            {
                var task2 = Task.Run(() => client.sendWaterParameterAsync(Encoding.UTF8.GetString(e.Message)));
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
