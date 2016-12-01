using System;
using System.Net;
using System.Text;
using System.Timers;
using uPLibrary.Networking.M2Mqtt;

namespace SmartH2O_DU
{

    class Program
    {
        private static SensorNodeDll.SensorNodeDll dll;
        private static HandlerXml handler = new HandlerXml();
        private static MqttClient m_cClient = new MqttClient("127.0.0.1");
       



        static void Main(string[] args)
        {
            try
            {
                dll = new SensorNodeDll.SensorNodeDll();
                dll.Initialize(readDataFromDll, Properties.Settings.Default.Delay);

                m_cClient.Connect(Guid.NewGuid().ToString());
                if (!m_cClient.IsConnected)
                {
                    Console.WriteLine("Error connecting to message broker...");
                    return;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void readDataFromDll(string message)
        {
            String xml = handler.createParameter(message);
            publishParameters(xml);
            //Console.WriteLine(xml);
        }

        private static void publishParameters(String parameter)
        {
            m_cClient.Publish("parameters", Encoding.UTF8.GetBytes(parameter));

        }



    }
}
