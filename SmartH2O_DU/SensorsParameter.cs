using System;


namespace SmartH2O_DU
{
    class SensorsParameter
    {
        public String name { get; set; }
        public String value { get; set; }
        public String id { get; set; }


        public SensorsParameter(String id,String name,String value)
        {
            this.id = id;
            this.name = name;
            this.value = value;
        }
    }
}
