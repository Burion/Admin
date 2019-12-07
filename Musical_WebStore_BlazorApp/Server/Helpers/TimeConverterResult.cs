using System;

namespace Admin.Helpers
{
    
    public class TimeConverterResult
    {

        public TimeConverterResult(int Days, int Hours, int Minutes, int Seconds)
        {   
            this.Days = Days;
            this.Hours = Hours;
            this.Minutes = Minutes;
            this.Seconds = Seconds;
        }
        public int Days {get;set;}
        public int Hours {get;set;}
        public int Minutes {get;set;}
        public int Seconds {get;set;}
    }

}