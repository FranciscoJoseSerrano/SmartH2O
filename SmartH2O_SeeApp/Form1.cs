using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SmartH2O_SeeApp
{

    public partial class Form1 : Form
    {
        private StatsService.SmartH20ServiceClient client = new StatsService.SmartH20ServiceClient();
        XmlDocument doc = new XmlDocument();
        public Form1()
        {
            InitializeComponent();
            fillAlarmTables();

        }

        private void fillAlarmTables()
        {
            tableAlarms.Controls.Add(new Label() { Text = "Day" }, tableAlarms.ColumnCount - 8, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 8, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Hour" }, tableAlarms.ColumnCount - 7, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 7, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Minute" }, tableAlarms.ColumnCount - 6, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 6, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Second" }, tableAlarms.ColumnCount - 5, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 5, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Parameter" }, tableAlarms.ColumnCount - 4, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 4, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Condition" }, tableAlarms.ColumnCount - 3, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 3, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Value" }, tableAlarms.ColumnCount - 2, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 2, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Message" }, tableAlarms.ColumnCount - 1, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 1, 0).BackColor = Color.Gray;
        }

        private void buttonSearch_Click(object sender, System.EventArgs e)
        {
            if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                MessageBox.Show("A primeira data tem de ser menor que a segunda!!!");
                return;
            }

            tableAlarms.Hide();
            clearTable();

            //<?xml version="1.0"?><alarms><date day="11" month="12" year="2016"><time hour="11" minute="46" second="8"><parameter>PH</parameter><value>8.0</value><alarm_message>DDWDWDDD</alarm_message><alarm_condition>beetween_min</alarm_condition></time></date></alarms>

            DateTimePicker dt1 = dateTimePicker1;
            DateTimePicker dt2 = dateTimePicker2;
            var xml = Task.Run(() => client.GetThresholdAlarmAsync(dt1.Value.Year.ToString(), dt1.Value.Month.ToString(),
                dt1.Value.Day.ToString(), dt2.Value.Year.ToString(), dt2.Value.Month.ToString(), dt2.Value.Day.ToString()));
            xml.Wait();
            if (xml.Result.CompareTo("") != 0)
            {
                doc.LoadXml(xml.Result);
                XmlNodeList days = doc.SelectNodes("/alarms/date");
                if (days == null || days.Count == 0)
                {
                    return;
                }

                foreach (XmlNode n in days)
                {
                    string day = n.Attributes["day"].InnerText;
                    foreach (XmlNode node in n.SelectNodes("time"))
                    {
                        tableAlarms.RowCount = tableAlarms.RowCount + 1;
                        tableAlarms.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                        tableAlarms.Controls.Add(new Label() { Text = day }, tableAlarms.ColumnCount - 8, tableAlarms.RowCount);
                        tableAlarms.Controls.Add(new Label() { Text = node.Attributes["hour"].InnerText + "h" }, tableAlarms.ColumnCount - 7, tableAlarms.RowCount);
                        tableAlarms.Controls.Add(new Label() { Text = node.Attributes["minute"].InnerText + "m" }, tableAlarms.ColumnCount - 6, tableAlarms.RowCount);
                        tableAlarms.Controls.Add(new Label() { Text = node.Attributes["second"].InnerText + "s" }, tableAlarms.ColumnCount - 5, tableAlarms.RowCount);
                        tableAlarms.Controls.Add(new Label() { Text = node["parameter"].InnerText }, tableAlarms.ColumnCount - 4, tableAlarms.RowCount);
                        tableAlarms.Controls.Add(new Label() { Text = node["alarm_condition"].InnerText }, tableAlarms.ColumnCount - 3, tableAlarms.RowCount);
                        tableAlarms.Controls.Add(new Label() { Text = node["value"].InnerText }, tableAlarms.ColumnCount - 2, tableAlarms.RowCount);
                        tableAlarms.Controls.Add(new Label() { Text = node["alarm_message"].InnerText , Width=200}, tableAlarms.ColumnCount - 1, tableAlarms.RowCount);        
                    }
                }
            }
            tableAlarms.Show();
        }


        private void clearTable()
        {
            for (int i = 1; i < tableAlarms.RowCount; i++)
            {
                for (int c = 0; c < tableAlarms.ColumnCount; c++)
                {
                    var control = tableAlarms.GetControlFromPosition(c, i);
                    tableAlarms.Controls.Remove(control);
                }

            }

        }
    }
}
