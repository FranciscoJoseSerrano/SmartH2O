using System;

namespace SmartH2O_DU
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int delay = 1000;
            SensorNodeDll.SensorNodeDll dll = new  SensorNodeDll.SensorNodeDll();
            dll.Initialize(readDataFromDll, delay);

        }

        private static void readDataFromDll(string message)
        {
            Console.WriteLine(message);
        }
    }
}
