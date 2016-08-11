using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UsageData
    {
        public string Name { get; set; }
        public int processorUsage { get; set; }
        public int memoryUsage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
