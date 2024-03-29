﻿using System;
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




        private XmlNode date;

        private string triggerRulesXSDPath = AppDomain.CurrentDomain.BaseDirectory + "trigger_rules.xsd";
        private string alarmXSDPath = AppDomain.CurrentDomain.BaseDirectory + "alarm.xsd";
        private string ValidationMessage { get; set; }
        private Boolean isValid;
        public string alarm { get; set; }
        XmlDocument docWaterParams;



        public HandlerXML()
        {

        }


        public void readTriggerRules(string triggerRulesXMLPath)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(triggerRulesXMLPath);
            if (isRulesXMLValid(doc))
            {


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


        }

        public void readXmlFile(string message)
        {
            docWaterParams = new XmlDocument();
            docWaterParams.LoadXml(message);


            date = docWaterParams.SelectSingleNode("/h2o");
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

        private bool isAlarmXMLValid(string xml)
        {

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            isValid = true;
            ValidationMessage = "Document Valid";
            try
            {
                ValidationEventHandler eventHandler = new ValidationEventHandler(MyEvent);
                xmlDocument.Schemas.Add(null, alarmXSDPath);
                xmlDocument.Validate(eventHandler);
            }
            catch (XmlException ex)
            {
                isValid = false;
                ValidationMessage = String.Format("Invalid Document {0}", ex.Message);
            }
            return isValid;
        }


        public bool isRulesXMLValid(XmlDocument xmlDocument)
        {
            isValid = true;
            ValidationMessage = "Document Valid";
            try
            {
                ValidationEventHandler eventHandler = new ValidationEventHandler(MyEvent);
                xmlDocument.Schemas.Add(null, triggerRulesXSDPath);
                xmlDocument.Validate(eventHandler);
            }
            catch (XmlException ex)
            {
                isValid = false;
                ValidationMessage = String.Format("Invalid Document {0}", ex.Message);
            }
            return isValid;
        }

        private void MyEvent(object sender, ValidationEventArgs e)
        {
            isValid = false;
            ValidationMessage = "Document is invalid" + e.Message;
        }

        public void comparePH(XmlNode node)
        {
            string PH_water_value = node["value"].InnerText;
            string message_rule_less = "value of PH (" + PH_water_value + ") is not less then " + PH_less_then + ".";
            string message_rule_equal = "value of PH (" + PH_water_value + ") is not equal " + PH_equal + ".";
            string message_rule_greater = "value of PH (" + PH_water_value + ") is not greater then " + PH_less_then + ".";
            string message_rule_between = "value of PH (" + PH_water_value + ") is not between " + PH_between_min + " and " + PH_between_max + ".";

            if (PH_less_then_condition == "ACTIVE")
            {
                if (!(float.Parse(PH_water_value.Replace(".", ",")) < float.Parse(PH_less_then)))
                {
                    AddAlarmAtribute(node, RULE_LESS, message_rule_less);
                }
            }
            if (PH_equal_condition == "ACTIVE")
            {
                if (!(float.Parse(PH_water_value.Replace(".", ",")) == float.Parse(PH_equal)))
                {
                    AddAlarmAtribute(node, RULE_EQUAL, message_rule_equal);
                }
            }
            if (PH_greater_then_condition == "ACTIVE")
            {
                if (!(float.Parse(PH_water_value.Replace(".", ",")) > float.Parse(PH_greater_then)))
                {
                    AddAlarmAtribute(node, RULE_GREATER, message_rule_greater);
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
                        AddAlarmAtribute(node, RULE_BETWEEN_MIN, message_rule_between);
                    }
                    else if (!(value <= max))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MAX, message_rule_between);
                    }

                }
            }
        }

        public void compareNH3(XmlNode node)
        {
            string NH3_water_value = node["value"].InnerText;
            string message_rule_less = "value of NH3 (" + NH3_water_value + ") is not less then " + NH3_less_then + ".";
            string message_rule_equal = "value of NH3 (" + NH3_water_value + ") is not equal " + NH3_equal + ".";
            string message_rule_greater = "value of NH3 (" + NH3_water_value + ") is not greater then " + NH3_less_then + ".";
            string message_rule_between = "value of NH3 (" + NH3_water_value + ") is not between " + NH3_between_min + " and " + NH3_between_max + ".";

            if (NH3_less_then_condition == "ACTIVE")
            {
                if (!(float.Parse(NH3_water_value.Replace(".", ",")) < float.Parse(NH3_less_then)))
                {
                    AddAlarmAtribute(node, RULE_LESS, message_rule_less);
                }

            }
            if (NH3_equal_condition == "ACTIVE")
            {
                if (!(float.Parse(NH3_water_value.Replace(".", ",")) == float.Parse(NH3_equal)))
                {
                    AddAlarmAtribute(node, RULE_EQUAL, message_rule_equal);
                }

            }
            if (NH3_greater_then_condition == "ACTIVE")
            {
                if (!(float.Parse(NH3_water_value.Replace(".", ",")) > float.Parse(NH3_greater_then)))
                {
                    AddAlarmAtribute(node, RULE_GREATER, message_rule_greater);
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
                        AddAlarmAtribute(node, RULE_BETWEEN_MIN, message_rule_between);
                    }
                    else if (!(value <= max))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MAX, message_rule_between);
                    }

                }

            }
        }

        public void compareCI2(XmlNode node)
        {
            string CI2_water_value = node["value"].InnerText;

            string message_rule_less = "value of CI2 (" + CI2_water_value + ") is not less then " + CI2_less_then + ".";
            string message_rule_equal = "value of CI2 (" + CI2_water_value + ") is not equal " + CI2_equal + ".";
            string message_rule_greater = "value of CI2 (" + CI2_water_value + ") is not greater then " + CI2_less_then + ".";
            string message_rule_between = "value of CI2 (" + CI2_water_value + ") is not between " + CI2_between_min + " and " + CI2_between_max + ".";

            if (CI2_less_then_condition == "ACTIVE")
            {
                if (!(float.Parse(CI2_water_value.Replace(".", ",")) < float.Parse(CI2_less_then)))
                {
                    AddAlarmAtribute(node, RULE_LESS, message_rule_less);
                }
            }
            if (CI2_equal_condition == "ACTIVE")
            {
                if (!(float.Parse(CI2_water_value.Replace(".", ",")) == float.Parse(CI2_equal)))
                {
                    AddAlarmAtribute(node, RULE_EQUAL, message_rule_equal);
                }

            }
            if (CI2_greater_then_condition == "ACTIVE")
            {
                if (!(float.Parse(CI2_water_value.Replace(".", ",")) > float.Parse(CI2_greater_then)))
                {
                    AddAlarmAtribute(node, RULE_GREATER, message_rule_greater);
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
                        AddAlarmAtribute(node, RULE_BETWEEN_MIN, message_rule_between);
                    }
                    else if (!(value <= max))
                    {
                        AddAlarmAtribute(node, RULE_BETWEEN_MAX, message_rule_between);
                    }

                }

            }


        }

        private bool isBetween(float min, float value, float max)
        {
            return (value.CompareTo(min) >= 0.0) && (value.CompareTo(max) <= 0.0);
        }


        private void AddAlarmAtribute(XmlNode node, string alarmCondition, string alarmMessage)
        {



            string year_value = date.Attributes["year"].InnerText;
            string month_value = date.Attributes["month"].InnerText;
            string day_value = date.Attributes["day"].InnerText;
            string hour_value = date.Attributes["hour"].InnerText;
            string minute_value = date.Attributes["minute"].InnerText;
            string second_value = date.Attributes["second"].InnerText;


            XmlAttribute alarm_condition = docWaterParams.CreateAttribute("alarm_condition");
            XmlAttribute year = docWaterParams.CreateAttribute("year");
            XmlAttribute month = docWaterParams.CreateAttribute("month");
            XmlAttribute day = docWaterParams.CreateAttribute("day");
            XmlAttribute hour = docWaterParams.CreateAttribute("hour");
            XmlAttribute minute = docWaterParams.CreateAttribute("minute");
            XmlAttribute second = docWaterParams.CreateAttribute("second");
            XmlElement alarm_message = docWaterParams.CreateElement("alarm_message");


            alarm_condition.Value = alarmCondition;
            year.Value = year_value;
            month.Value = month_value;
            day.Value = day_value;
            hour.Value = hour_value;
            minute.Value = minute_value;
            second.Value = second_value;
            alarm_message.InnerText = alarmMessage;


            node.Attributes.Append(alarm_condition);
            node.Attributes.Append(year);
            node.Attributes.Append(month);
            node.Attributes.Append(day);
            node.Attributes.Append(hour);
            node.Attributes.Append(minute);
            node.Attributes.Append(second);
            node.AppendChild(alarm_message);

            string xml = node.OuterXml;
            if (isAlarmXMLValid(xml))
            {
                alarm = xml;

            }
        }
    }

}

