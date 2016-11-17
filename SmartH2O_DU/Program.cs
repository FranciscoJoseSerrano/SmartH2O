using System;
using System.Timers;

namespace SmartH2O_DU
{

    class Program
    {
        private static int delay = 5000;
        private static SensorNodeDll.SensorNodeDll dll;
        private static HandlerXml handler = new HandlerXml();


        static void Main(string[] args)
        {
            try
            {
                dll = new SensorNodeDll.SensorNodeDll();
                dll.Initialize(readDataFromDll, delay);

                

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        private static void handlerReceivingInformation(object sender, ElapsedEventArgs e)
        {
            dll = new SensorNodeDll.SensorNodeDll();

            dll.Initialize(readDataFromDll, delay);
        }

        private static void readDataFromDll(string message)
        {
            handler.createParameter(message);
        }


    }
}
