﻿using System;
using System.Timers;

namespace SmartH2O_DU
{

    class Program
    {
        private static SensorNodeDll.SensorNodeDll dll;
        private static HandlerXml handler = new HandlerXml();

        static void Main(string[] args)
        {
            try
            {
                dll = new SensorNodeDll.SensorNodeDll();
                dll.Initialize(readDataFromDll, Properties.Settings.Default.Delay);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void readDataFromDll(string message)
        {
            String xml = handler.createParameter(message);
            Console.WriteLine(xml);
        }


    }
}
