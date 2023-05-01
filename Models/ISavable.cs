using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoTime.Models
{
    internal interface ISavable
    {
        void saveToFile<T>(ConfigAPP configAPP, T configTime );
    }
}
