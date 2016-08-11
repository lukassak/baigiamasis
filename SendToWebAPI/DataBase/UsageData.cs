using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UsageData
    {
        public int UsageDataId { get; set; }
        public int CpuUsage { get; set; }
        public int RamUsage { get; set; }
        public int AvailableDiskSpaceGb { get; set; }
        public int AverageDiskQueueLength { get; set; }
        public DateTime? Time { get; set; }
    }
}
