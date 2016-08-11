using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DataLayer
{
    public abstract class DataManager
    {
        public virtual string GetMetric(string type)
        {
            var value = "";
            switch (type.ToLower())
            {
                 case "computername":
                              value = Environment.MachineName;
                                break;

                    case "cpuusage":


                                  var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                    
                                   Random r = new Random();
                                   int n = r.Next(0, 21);
                                   value = n.ToString();
                    
                    break;


                case "ramusage":
                                     ManagementObjectSearcher searcher2 =
                                     new ManagementObjectSearcher(@"root\CIMV2",
                                                                                   "SELECT * FROM Win32_OperatingSystem");



                    foreach (ManagementObject queryObj in searcher2.Get())
                    {
                               double free = Double.Parse(queryObj["FreePhysicalMemory"].ToString());
                                 double total = Double.Parse(queryObj["TotalVisibleMemorySize"].ToString());
                              value = Math.Round((total - free) / total * 100, 0).ToString();
                    }

                    break;

            }

            return value;
        }

          public abstract UsageData GetComputerSummary();
      
    }

  
}
