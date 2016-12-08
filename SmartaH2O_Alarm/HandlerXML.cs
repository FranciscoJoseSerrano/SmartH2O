using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace SmartaH2O_Alarm
{
    class HandlerXML
    {


        private string PH_less_then_condition;
        private string PH_less_then;
        private string PH_equal_condition;
        private string PH_equal;
        private string PH_greater_then_condition;
        private string PH_greater_then;
        private string PH_between_condition;
        private string PH_between_min;
        private string PH_between_max;

        private string NH3_less_then_condition;
        private string NH3_less_then;
        private string NH3_equal_condition;
        private string NH3_equal;
        private string NH3_greater_then_condition;
        private string NH3_greater_then;
        private string NH3_between_condition;
        private string NH3_between_min;
        private string NH3_between_max;

        private string CI2_less_then_condition;
        private string CI2_less_then;
        private string CI2_equal_condition;

        private string CI2_equal;
        private string CI2_greater_then_condition;
        private string CI2_greater_then;
        private string CI2_between_condition;
        private string CI2_between_min;
        private string CI2_between_max;


        private static string PARAM_PH = "PH";
        private static string PARAM_NH3 = "NH3";
        private static string PARAM_CI2 = "CI2";
        private static string RULE_LESS = "less_then";
        private static string RULE_GREATER = "greater_then";
        private static string RULE_EQUAL = "equal";
        private static string RULE_BETWEEN_MAX = "between_max";
        private static string RULE_BETWEEN_MIN = "beetween_min";

        public string alarm { get; set; }



        XmlDocument docWaterParams;
        private string XMLFileWaterAlarmsPath = AppDomain.CurrentDomain.BaseDirectory + "water_parameters_alarms.xml";





        public HandlerXML()
        {

        }


        public void readTriggerRules(string triggerRulesXMLPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(triggerRulesXMLPath);


            PH_less_then_condition = doc.SelectSingleNode("rules/parameter[@name='PH']/rule/less_then/@condition").InnerText;
            PH_less_then = doc.SelectSingleNode("rules/parameter[@name='PH']/rule/less_then").InnerText;
            PH_equal_condition = doc.SelectSingleNode("rules/parameter[@name ='PH']/rule/equal/@condition").InnerText;
            PH_equal = doc.SelectSingleNode("rules/parameter[@name ='PH']/rule/equal").InnerText;
            PH_greater_then_condition = doc.SelectSingleNode("rules/parameter[@name='PH']/rule/greater_then/@condition").InnerText;
            PH_greater_then = doc.SelectSingleNode("rules/parameter[@name='PH']/rule/greater_then").InnerText;
            PH_between_condition = doc.SelectSingleNode("rules/parameter[@name='PH']/rule/between/@condition").InnerText;
            PH_between_min = doc.SelectSingleNode("rules/parameter[@name='PH']/rule/between/min").InnerText;
            PH_between_max = doc.SelectSingleNode("rules/parameter[@name='PH']/rule/between/max").InnerText;

            NH3_less_then_condition = doc.SelectSingleNode("rules/parameter[@name='NH3']/rule/less_then/@condition").InnerText;
            NH3_less_then = doc.SelectSingleNode("rules/parameter[@name='NH3']/rule/less_then").InnerText;
            NH3_equal_condition = doc.SelectSingleNode("rules/parameter[@name ='NH3']/rule/equal/@condition").InnerText;
            NH3_equal = doc.SelectSingleNode("rules/parameter[@name ='NH3']/rule/equal").InnerText;
            NH3_greater_then_condition = doc.SelectSingleNode("rules/parameter[@name='NH3']/rule/greater_then/@condition").InnerText;
            NH3_greater_then = doc.SelectSingleNode("rules/parameter[@name='NH3']/rule/greater_then").InnerText;
            NH3_between_condition = doc.SelectSingleNode("rules/parameter[@name='NH3']/rule/between/@condition").InnerText;
            NH3_between_min = doc.SelectSingleNode("rules/parameter[@name='NH3']/rule/between/min").InnerText;
            NH3_between_max = doc.SelectSingleNode("rules/parameter[@name='NH3']/rule/between/max").InnerText;


            CI2_less_then_condition = doc.SelectSingleNode("rules/parameter[@name='CI2']/rule/less_then/@condition").InnerText;
            CI2_less_then = doc.SelectSingleNode("rules/parameter[@name='CI2']/rule/less_then").InnerText;
            CI2_equal_condition = doc.SelectSingleNode("rules/parameter[@name ='CI2']/rule/equal/@condition").InnerText;
            CI2_equal = doc.SelectSingleNode("rules/parameter[@name ='CI2']/rule/equal").InnerText;
            CI2_greater_then_condition = doc.SelectSingleNode("rules/parameter[@name='CI2']/rule/greater_then/@condition").InnerText;
            CI2_greater_then = doc.SelectSingleNode("rules/parameter[@name='CI2']/rule/greater_then").InnerText;
            CI2_between_condition = doc.SelectSingleNode("rules/parameter[@name='CI2']/rule/between/@condition").InnerText;
            CI2_between_min = doc.SelectSingleNode("rules/parameter[@name='CI2']/rule/between/min").InnerText;
            CI2_between_max = doc.SelectSingleNode("rules/parameter[@name='CI2']/rule/between/max").InnerText;

        }

        public void readXmlFile(string message)
        {
            docWaterParams = new XmlDocument();
            docWaterParams.LoadXml(message);
            XmlNode param = docWaterParams.SelectSingleNode("/h2o/parameter");
            if (param.Attributes["name"].InnerText == PARAM_PH)
            {
                comparePH(param);
            }
            else if (param.Attributes["name"].InnerText == PARAM_NH3)
            {
                compareNH3(param);
            }
            else if (param.Attributes["name"].InnerText == PARAM_CI2)
            {
                compareCI2(param);
            }
        }

        public void comparePH(XmlNode node)
        {
            string PH_water_value = node["value"].InnerText;
            if (PH_less_then_condition == "ACTIVE")
            {
                if (!(float.Parse(PH_water_value.Replace(".", ",")) < float.Parse(PH_less_then)))
                {
                    AddAlarmAtribute(node, RULE_LESS);
                }
            }
            if (PH_equal_condition == "ACTIVE")
            {
                if (!(float.Parse(PH_water_value.Replace(".", ",")) == float.Parse(PH_equal)))
                {
                    AddAlarmAtribute(node, RULE_EQUAL);
                }
            }
            if (PH_greater_then_condition == "ACTIVE")
            {
                if (!(float.Parse(PH_water_value.Replace(".", ",")) > float.Parse(PH_greater_then)))
                {
                    AddAlarmAtribute(node, RULE_GREATER);
                }
            }
            if (PH_between_condition == "ACTIVE")
            {
                float min = float.Parse(PH_between_min);
                float value = float.Parse(PH_water_value.Replace(".", ","));
                float max = float.Parse(PH_between_max);

                if (!(isBetween(min, value, max)))
                {
                    if (!(value >= min))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MIN);
                    }
                    else if (!(value <= max))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MAX);
                    }

                }
            }
        }

        public void compareNH3(XmlNode node)
        {
            string NH3_water_value = node["value"].InnerText;
            if (NH3_less_then_condition == "ACTIVE")
            {
                if (!(float.Parse(NH3_water_value.Replace(".", ",")) < float.Parse(NH3_less_then)))
                {
                    AddAlarmAtribute(node, RULE_LESS);
                }

            }
            if (NH3_equal_condition == "ACTIVE")
            {
                if (!(float.Parse(NH3_water_value.Replace(".", ",")) == float.Parse(NH3_equal)))
                {
                    AddAlarmAtribute(node, RULE_EQUAL);
                }

            }
            if (NH3_greater_then_condition == "ACTIVE")
            {
                if (!(float.Parse(NH3_water_value.Replace(".", ",")) > float.Parse(NH3_greater_then)))
                {
                    AddAlarmAtribute(node, RULE_GREATER);
                }

            }
            if (NH3_between_condition == "ACTIVE")
            {
                float min = float.Parse(NH3_between_min);
                float value = float.Parse(NH3_water_value.Replace(".", ","));
                float max = float.Parse(NH3_between_max);

                if (!(isBetween(min, value, max)))
                {
                    if (!(value >= min))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MIN);
                    }
                    else if (!(value <= max))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MAX);
                    }

                }

            }
        }

        public void compareCI2(XmlNode node)
        {
            string CI2_water_value = node["value"].InnerText;
            if (CI2_less_then_condition == "ACTIVE")
            {
                if (!(float.Parse(CI2_water_value.Replace(".", ",")) < float.Parse(CI2_less_then)))
                {
                    AddAlarmAtribute(node, RULE_LESS);
                }
            }
            if (CI2_equal_condition == "ACTIVE")
            {
                if (!(float.Parse(CI2_water_value.Replace(".", ",")) == float.Parse(CI2_equal)))
                {
                    AddAlarmAtribute(node, RULE_EQUAL);
                }

            }
            if (CI2_greater_then_condition == "ACTIVE")
            {
                if (!(float.Parse(CI2_water_value.Replace(".", ",")) > float.Parse(CI2_greater_then)))
                {
                    AddAlarmAtribute(node, RULE_GREATER);
                }

            }
            if (CI2_between_condition == "ACTIVE")
            {
                float min = float.Parse(CI2_between_min);
                float value = float.Parse(CI2_water_value.Replace(".", ","));
                float max = float.Parse(CI2_between_max);

                if (!(isBetween(min, value, max)))
                {
                    if (!(value >= min))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MIN);
                    }
                    else if (!(value <= max))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MAX);
                    }

                }

            }


        }

        private bool isBetween(float min, float value, float max)
        {
            return (value.CompareTo(min) >= 0.0) && (value.CompareTo(max) <= 0.0);
        }

        private void AddAlarmAtribute(XmlNode node, string alarmCondition)
        {
            XmlAttribute alarm_condition = docWaterParams.CreateAttribute("alarm_condition");
            alarm_condition.Value = alarmCondition;
            node.Attributes.Append(alarm_condition);
            alarm = node.OuterXml;
        }
    }

}

