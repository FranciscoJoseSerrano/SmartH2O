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
        private MqttClient m_cClient = new MqttClient("127.0.0.1");
        private string[] m_strTopicsInfo = { "parameters" };
        private byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
        private HandlerXML handlerXML = new HandlerXML();

        private string xmlFileRulesPath = AppDomain.CurrentDomain.BaseDirectory + "trigger_rules.xml";




        public Form1()
        {
            InitializeComponent();
            handlerXML.readTriggerRules(xmlFileRulesPath);
            subscribeParameter();
            
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            handlerXML.readXmlFile(Encoding.UTF8.GetString(e.Message));
            publishAlarms(handlerXML.alarm);
          
           
        }
        
        private void subscribeParameter()
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

        private void publishAlarms(String alarm)
        {
            m_cClient.Publish("alarms", Encoding.UTF8.GetBytes(alarm));

        }

        private void buttonOnOff_Click(object sender, EventArgs e)
        {
           
           
           if (buttonOnOff.Text == "ON")
            {
                handlerXML.readTriggerRules(xmlFileRulesPath);
                subscribeParameter();
                buttonOnOff.Text = "OFF";

            } else
            {
                unsubscribeParameter();
                buttonOnOff.Text = "ON";
            }
            
        }
    }

}
