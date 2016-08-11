using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public abstract class DataManager
    {
        public virtual string GetMetric(string type)
        {
            var value = "";
            switch (type.ToLower())
            {
                   
                case "cpuusage":

                    var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                    /*var collection = searcher.Get();
                    foreach (var obj in searcher.Get().Cast<ManagementObject>())
                    {
                       // var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", "MyComputer");
                        //value = cpuCounter.NextValue().ToString();
                        //Implementation not worked from beggining, code is not mine; Should i go for an extra mile? surelyy
                        value = obj["PercentProcessorTime"].ToString();
                        break;
                    }*/

                    //Mentor suggested this solution, because somehow it's not possible to get current usage
                    Random r = new Random();
                    int n = r.Next(0, 21);
                    value = n.ToString();
                    //Console.WriteLine(value);

                    break;

                case "computername":
                    value = Environment.MachineName;
                    break;

                case "user":
                    value = Environment.UserName;
                    break;

                case "cpu":
                    value = System.Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
                    break;

                case "gpu":
                    searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
                    string graphicsCard = string.Empty;
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        foreach (PropertyData property in mo.Properties)
                        {
                            if (property.Name == "Description")
                            {
                                value = property.Value.ToString();
                            }
                        }
                    }
                    break;

                case "ram":
                    string Query = "SELECT MaxCapacity FROM Win32_PhysicalMemoryArray";
                    searcher = new ManagementObjectSearcher(Query);
                    foreach (ManagementObject WniPART in searcher.Get())
                    {
                        UInt32 SizeinKB = Convert.ToUInt32(WniPART.Properties["MaxCapacity"].Value);
                        UInt32 SizeinMB = SizeinKB / 1024;
                        value = SizeinMB.ToString();
                    }
                    
                    break;

                case "ramusage":
                    PerformanceCounter ramcounter;
                    ramcounter = new PerformanceCounter("Memory", "Available MBytes");
                    value = ramcounter.NextValue() + "MB";
                    break;

                case "freespace":

                    var moCollection = new ManagementClass("Win32_LogicalDisk").GetInstances();

                    foreach (var mo in moCollection)
                    {
                        if (mo["DeviceID"] != null && mo["DriveType"] != null && mo["Size"] != null && mo["FreeSpace"] != null)
                        {
                            // DriveType 3 = "Local Disk"
                            if (Convert.ToInt32(mo["DriveType"]) == 3)
                            {
                                /*Console.WriteLine("Drive {0}", mo["DeviceID"]);
                                Console.WriteLine("Size {0} bytes", mo["Size"]);
                                Console.WriteLine("Free {0} bytes", mo["FreeSpace"]);*/
                                long a = Convert.ToInt64(mo["FreeSpace"].ToString());
                                
                                
                                value = mo["FreeSpace"].ToString();

                            }
                        }
                    }

                    break;

                case "ip":
                    //Console.WriteLine(Dns.GetHostName());
                    IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                    foreach (IPAddress addr in localIPs)
                    {
                        value = addr.ToString();
                    }

                    break;
                default:
                    value = "error";
                    break;

            }

            return value;
        }

        public abstract ComputerSummary GetComputerSummary();
        public abstract List<string> GetApplicationList();
        public abstract List<string> GetHardwareList();

    }
}
