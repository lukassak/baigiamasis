using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ComputerSummary
    {

       public string Name { get; set; }
        public string User { get; set; }
        public string Cpu { get; set; }
        public int Ram { get; set; } 
        public int CpuUsage { get; set; }
        public int RamUsage { get; set; }
       

    }
}
