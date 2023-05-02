using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoTime.Models
{
    public interface ISavable
    {
        void saveToFile(ConfigAPP configAPP);
    }
}
