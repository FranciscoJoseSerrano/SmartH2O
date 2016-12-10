using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartH2O_SeeApp
{
    public partial class Form2 : Form
    {
        Chart[] dailyCharts = new Chart[3];
        Chart[] weeklyCharts = new Chart[3];
        public Form2()
        {
            InitializeComponent();
            initializeCharts();
            fillComboBoxes();
            fillDailyCharts();
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

       

        private void fillDailyCharts()
        {

            dailyCharts[0].Series[0].Points.AddXY(1, 2);
            dailyCharts[0].Series[1].Points.AddXY(1, 4);
            dailyCharts[0].Series[2].Points.AddXY(1, 6);

            dailyCharts[0].Series[0].Points.AddXY(2, 2);
            dailyCharts[0].Series[1].Points.AddXY(2, 4);
            dailyCharts[0].Series[2].Points.AddXY(2, 6);



            dailyCharts[1].Series[0].Points.AddXY(1, 2);
            dailyCharts[1].Series[1].Points.AddXY(1, 4);
            dailyCharts[1].Series[2].Points.AddXY(1, 6);

            dailyCharts[1].Series[0].Points.AddXY(2, 2);
            dailyCharts[1].Series[1].Points.AddXY(2, 4);
            dailyCharts[1].Series[2].Points.AddXY(2, 6);

            dailyCharts[2].Series[0].Points.AddXY(1, 2);
            dailyCharts[2].Series[1].Points.AddXY(1, 4);
            dailyCharts[2].Series[2].Points.AddXY(1, 6);

            dailyCharts[2].Series[0].Points.AddXY(2, 2);
            dailyCharts[2].Series[1].Points.AddXY(2, 4);
            dailyCharts[2].Series[2].Points.AddXY(2, 6);



        }

        private void fillComboBoxes()
        {

            for (int i = 1; i < 13; i++)
            {
                if (i > 9)
                {
                    comboBoxMonths.Items.Add(i.ToString());
                }
                else
                {
                    comboBoxMonths.Items.Add("0" + i.ToString());
                }

            }

            comboBoxYears.Items.Add("2016");
            comboBoxYears.Items.Add("2017");

            comboBoxMonths.SelectedItem = DateTime.Now.Month.ToString();
            comboBoxYears.SelectedItem = DateTime.Now.Year.ToString();
        }
    }
}
