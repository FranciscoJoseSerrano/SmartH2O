using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.ServiceReference1;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private ServiceReference1.SmartH20ServiceClient client;
        
        
        public Form1()
        {
            InitializeComponent();
            client = new SmartH20ServiceClient();
      
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatePerHour date = new DatePerHour();
            
            
            DatePerHour[] list = client.GetHourlyInSpecificDay("2016", "12", "1", "PH");

            foreach (DatePerHour item in list)
            {
                MessageBox.Show("2016/12/1" + "PH : " + item.hour + "/" + item.value + "MAX :" + item);
            }
        }
    }
}
