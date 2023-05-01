using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoTime.Models
{
    internal interface ISavable
    {
        void saveToFile(ConfigAPP configAPP, ISavable config );
    }
}
