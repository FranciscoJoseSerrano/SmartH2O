using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartaH2O_Alarm
{
    public partial class Form1 : Form
    {
        private MqttClient m_cClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
        private string[] m_strTopicsInfo = { "parameters" };
        private byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
     

        public Form1()
        {
            InitializeComponent();
            subscriveParameter();
            
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
          MessageBox.Show("Received = " + Encoding.UTF8.GetString(e.Message) +
            " on topic " + e.Topic);
        }
        
        private void subscriveParameter()
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


        private void unsubscribeParameter()
        {
            if (m_cClient.IsConnected)
            {
                m_cClient.Unsubscribe(m_strTopicsInfo);
                m_cClient.Disconnect();
            }
        }

        private void buttonOnOff_Click(object sender, EventArgs e)
        {
            subscriveParameter();
           
           if (buttonOnOff.Text == "ON")
            {

                buttonOnOff.Text = "OFF";

            } else
            {
                unsubscribeParameter();
                buttonOnOff.Text = "ON";
            }
            
        }
    }

}
