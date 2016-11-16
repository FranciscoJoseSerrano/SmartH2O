using System;
using System.Timers;

namespace SmartH2O_DU
{

    class Program
    {
        private static int count = 0;
        private static Timer time = new Timer(10000);
        private static int delay = 500;
        private static SensorNodeDll.SensorNodeDll dll;
        private static String[] parameters = new String[3];


        static void Main(string[] args)
        {

            try
            {
                /** FIRST ARRAY OF INFORMATION **/
                dll = new SensorNodeDll.SensorNodeDll();
                dll.Initialize(readDataFromDll, delay);

                time.Elapsed += new ElapsedEventHandler(handlerReceivingInformation);
                time.Start();
                while (Console.Read() != 's') ;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            finally
            {
                time.Dispose();
            }



        }

        private static void handlerReceivingInformation(object sender, ElapsedEventArgs e)
        {
            dll = new SensorNodeDll.SensorNodeDll();

            dll.Initialize(readDataFromDll, delay);
        }

        private static void readDataFromDll(string message)
        {
            parameters[count] = message;
            count++;

            if (count == 3)
            {
                HandlerXml handler = new HandlerXml(parameters);
                dll.Stop();
                parameters = new String[3];
                count = 0;
            }
        }


    }
}
