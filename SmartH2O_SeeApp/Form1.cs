using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartH2O_SeeApp
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillAlarmTables();
      
        }


        private void tableAlarms_Paint(object sender, PaintEventArgs e)
        {
        }

        private void fillAlarmTables()
        {
            tableAlarms.Controls.Add(new Label() { Text = "Hour" }, tableAlarms.ColumnCount - 6, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 6, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Minute" }, tableAlarms.ColumnCount - 5, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 5, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Second" }, tableAlarms.ColumnCount - 4, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 4, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Parameter" }, tableAlarms.ColumnCount - 3, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 3, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Condition" }, tableAlarms.ColumnCount - 2, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 2, 0).BackColor = Color.Gray;
            tableAlarms.Controls.Add(new Label() { Text = "Value" }, tableAlarms.ColumnCount - 1, 0);
            tableAlarms.GetControlFromPosition(tableAlarms.ColumnCount - 1, 0).BackColor = Color.Gray;
        }

    }
}
