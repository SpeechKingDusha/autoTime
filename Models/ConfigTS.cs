using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoTime.Models
{
    public class ConfigTS
    {
        public string domainName { get; set; }
        public string userName { get; set; }
        public string  password { get; set; }
        public bool isTestedMode { get; set; }
    }
}
