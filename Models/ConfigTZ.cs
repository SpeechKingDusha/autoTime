using System;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace autoTime.Models
{
    public class ConfigTZ
    {
        public string URL { get; private set; } = "https://tz.mis.amdocs.com/weekly-report";
        public string Email { get; set; }
        public List<String> Holidays { get; set; }
        public string StartHoursDay { get; set; }
        public int HoursDay { get; set; }
        public int HoursWeekDay { get; set; }
        public bool isTestedMode { get; set; }
    }

}
