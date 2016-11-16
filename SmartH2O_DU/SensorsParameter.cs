using System;


namespace SmartH2O_DU
{
    class SensorsParameter
    {
        public String name { get; set; }
        public String value { get; set; }


        public SensorsParameter(String name,String value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
