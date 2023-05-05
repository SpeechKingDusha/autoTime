using autoTime.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace autoTime.Models
{
    public class ConfigTZ
    {
        public string Email { get; set; }
        public List<String> Holidays { get; set; }
        public string StartHoursDay { get; set; }
        public int HoursDay { get; set; }
        public int HoursWeekDay { get; set; }
        public bool isTestedMode { get; set; }
    }

}
