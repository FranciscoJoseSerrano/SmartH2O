using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace SmartH2O_SeeApp
{


    public partial class Form2 : Form
    {

        IDictionary<string, bool> parameters;

        Chart[] dailyCharts = new Chart[3];
        Chart[] weeklyCharts = new Chart[3];

        private StatsService.SmartH20ServiceClient client = new StatsService.SmartH20ServiceClient();
        public Form2()
        {
            InitializeComponent();
            parameters = new Dictionary<string, bool>();
            parameters.Add("PH", true);
            parameters.Add("CI2", true);
            parameters.Add("NH3", true);
            initializeCharts();
            getInicialValuesDailyCharts();
            getInicialValuesWeeklyCharts();
        }


        private void initializeCharts()
        {
            dailyCharts[0] = chartDailyPH;
            dailyCharts[1] = chartDailyNH3;
            dailyCharts[2] = chartDailyCI2;

            weeklyCharts[0] = chartWeeklyPH;
            weeklyCharts[1] = chartWeeklyNH3;
            weeklyCharts[2] = chartWeeklyCI2;

            foreach (Chart c in dailyCharts)
            {
                c.ChartAreas[0].AxisX.Title = "Hour";
                c.ChartAreas[0].AxisY.Title = "Value";
                c.Series[0].ChartType = SeriesChartType.Column;
            }

            foreach (Chart c in weeklyCharts)
            {
                c.ChartAreas[0].AxisX.Title = "Day";
                c.ChartAreas[0].AxisY.Title = "Value";
                c.Series[0].ChartType = SeriesChartType.Column;
            }

        }

        private void getInicialValuesDailyCharts()
        {
            //<?xml version="1.0"?><data><PH><date day="11" month="12" year="2016"><hour>11</hour><average>5,9</average><max>6,8</max><min>5,0</min></date>
            //min,max,average
            //PH,NH3,CI2

            foreach (var p in parameters.Keys)
            {
                var task = Task.Run(() => client.GetHourlyInSpecificDayParameterAsync(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), p.ToUpper()));
                task.Wait();
                fillDailyCharts(task.Result, p.ToUpper());
            }


        }

        private void getInicialValuesWeeklyCharts()
        {
            //<?xml version="1.0"?><data><PH><date day="10/12/2016 00:00:00"><average>5,9</average><max>6,8</max><min>5,1</min></day>

            var firstDayOfTheWeek = dateTimePickerWeekly.Value;
            DateTime lastDayOfTheWeek = dateTimePickerWeekly.Value.Date.AddDays(7);
            foreach (var p in parameters.Keys)
            {
                var task = Task.Run(() => client.GetDailyInThresholdParameterAsync(firstDayOfTheWeek.Year.ToString(), firstDayOfTheWeek.Month.ToString(), firstDayOfTheWeek.Day.ToString(),
                      lastDayOfTheWeek.Year.ToString(), lastDayOfTheWeek.Month.ToString(), lastDayOfTheWeek.Day.ToString(), p.ToUpper()));
                task.Wait();
                fillWeeklyCharts(task.Result, p.ToUpper());

            }
        }

        private void fillDailyCharts(string xml, string parameter)
        {
            int p = 0;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList list = doc.SelectNodes("/data/" + parameter + "/date");

            switch (parameter)
            {
                case "PH":
                    p = 0;
                    foreach (var series in chartDailyPH.Series)
                    {
                        series.Points.Clear();
                    }

                    if (list.Count == 0)
                    {
                        labelDailyPH.Visible = true;
                       
                    }
                    else
                    {
                        labelDailyPH.Visible = false;
                    }
                    break;
                case "NH3":
                    p = 1;
                    foreach (var series in chartDailyNH3.Series)
                    {
                        series.Points.Clear();
                    }

                    if (list.Count == 0)
                    {
                        labelDailyNH3.Visible = true;
                       
                    }
                    else
                    {
                        labelDailyNH3.Visible = false;
                    }
                    break;
                case "CI2":
                    p = 2;
                    foreach (var series in chartDailyCI2.Series)
                    {
                        series.Points.Clear();
                    }

                    if (list.Count == 0)
                    {
                        labelDailyCI2.Visible = true;
                       
                    }
                    else
                    {
                        labelDailyCI2.Visible = false;
                    }
                    break;
            }

            if (list.Count == 0)
            {
                return;
            }



            foreach (XmlNode n in list)
            {
                dailyCharts[p].Series[0].Points.AddXY(int.Parse(n["hour"].InnerText), double.Parse(n["min"].InnerText));
                dailyCharts[p].Series[1].Points.AddXY(int.Parse(n["hour"].InnerText), double.Parse(n["max"].InnerText));
                dailyCharts[p].Series[2].Points.AddXY(int.Parse(n["hour"].InnerText), double.Parse(n["average"].InnerText));

            }


        }

        private void fillWeeklyCharts(string xml, string parameter)
        {
            int p = 0;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList list = doc.SelectNodes("/data/" + parameter + "/date");

            switch (parameter)
            {
                case "PH":
                    p = 0;

                    foreach (var series in chartWeeklyPH.Series)
                    {
                        series.Points.Clear();
                    }
                    if (list.Count == 0)
                    {
                        labelWeeklyPh.Visible = true;
                       
                    }
                    else
                    {
                        labelWeeklyPh.Visible = false;
                    }
                    break;
                case "NH3":
                    p = 1;

                    foreach (var series in chartWeeklyNH3.Series)
                    {
                        series.Points.Clear();
                    }

                    if (list.Count == 0)
                    {
                        labelWeeklyNH3.Visible = true;
                        
                    }
                    else
                    {
                        labelWeeklyNH3.Visible = false;
                    }
                    break;
                case "CI2":
                    p = 2;
                    foreach (var series in chartWeeklyCI2.Series)
                    {
                        series.Points.Clear();
                    }

                    if (list.Count == 0)
                    {
                        labelWeeklyCI2.Visible = true;
                      
                    }
                    else
                    {
                        labelWeeklyCI2.Visible = false;
                    }
                    break;
            }

            if (list.Count == 0)
            {
                return;
            }

            foreach (XmlNode n in list)
            {
                weeklyCharts[p].Series[0].Points.AddXY(int.Parse(n.Attributes["day"].InnerText.Substring(0, 2)), double.Parse(n["min"].InnerText));
                weeklyCharts[p].Series[1].Points.AddXY(int.Parse(n.Attributes["day"].InnerText.Substring(0, 2)), double.Parse(n["max"].InnerText));
                weeklyCharts[p].Series[2].Points.AddXY(int.Parse(n.Attributes["day"].InnerText.Substring(0, 2)), double.Parse(n["average"].InnerText));

            }



        }

        private void getDailyValues()
        {
            foreach (var n in parameters)
            {
                if (n.Value)
                {
                    var task = Task.Run(() => client.GetHourlyInSpecificDayParameterAsync(dateTimePickerDay.Value.Year.ToString(),
                        dateTimePickerDay.Value.Month.ToString(), dateTimePickerDay.Value.Day.ToString(), n.Key.ToUpper()));
                    task.Wait();
                    fillDailyCharts(task.Result, n.Key.ToUpper());
                }
            }
        }

        private void getWeeklyValues()
        {
            var firstDayOfTheWeek = dateTimePickerWeekly.Value;
            DateTime lastDayOfTheWeek = dateTimePickerWeekly.Value.Date.AddDays(7);
            foreach (var n in parameters)
            {
                if (n.Value)
                {
                    var task = Task.Run(() => client.GetDailyInThresholdParameterAsync(firstDayOfTheWeek.Year.ToString(), firstDayOfTheWeek.Month.ToString(), firstDayOfTheWeek.Day.ToString(),
                      lastDayOfTheWeek.Year.ToString(), lastDayOfTheWeek.Month.ToString(), lastDayOfTheWeek.Day.ToString(), n.Key.ToUpper()));
                    task.Wait();
                    fillWeeklyCharts(task.Result, n.Key.ToUpper());
                }
            }
        }


        private void dateTimePickerDay_ValueChanged(object sender, EventArgs e)
        {
            getDailyValues();
        }


        private void checkBoxPH_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBoxPH.Checked)
            {
                parameters["PH"] = true;
                chartDailyPH.Visible = true;
                chartWeeklyPH.Visible = true;

            }
            else
            {
                parameters["PH"] = false;
                chartDailyPH.Visible = false;
                labelDailyPH.Visible = false;

                chartWeeklyPH.Visible = false;
                labelWeeklyPh.Visible = false;
            }
            getDailyValues();
        }


        private void checkBoxNh3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNh3.Checked)
            {
                parameters["NH3"] = true;
                chartDailyNH3.Visible = true;
                chartWeeklyNH3.Visible = true;

            }
            else
            {
                parameters["NH3"] = false;
                chartDailyNH3.Visible = false;
                labelDailyNH3.Visible = false;

                chartWeeklyNH3.Visible = false;
                labelWeeklyNH3.Visible = false;
            }
            getDailyValues();
        }


        private void checkBoxCi2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCi2.Checked)
            {
                parameters["CI2"] = true;
                chartDailyCI2.Visible = true;
                chartWeeklyCI2.Visible = true;

            }
            else
            {
                parameters["CI2"] = false;
                chartDailyCI2.Visible = false;
                labelDailyCI2.Visible = false;

                chartWeeklyCI2.Visible = false;
                labelWeeklyCI2.Visible = false;
            }
            getDailyValues();
        }

        private void dateTimePickerWeekly_ValueChanged(object sender, EventArgs e)
        {
            getWeeklyValues();
        }
    }
}
