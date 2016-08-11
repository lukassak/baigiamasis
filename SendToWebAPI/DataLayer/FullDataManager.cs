using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FullDataManager : DataManager
    {
        public override ComputerSummary GetComputerSummary()
        {
            ComputerSummary summ = new ComputerSummary();
            
            summ.Name = GetMetric("computername");
            summ.CpuUsage = int.Parse(GetMetric("cpuusage"));
            summ.User = GetMetric("User");
            summ.Cpu = GetMetric("cpu");
            summ.VideoCard = GetMetric("gpu");
            summ.Ram = int.Parse(GetMetric("ram"));
            summ.Ip = GetMetric("ip");
            string cancer = string.Join("", GetMetric("ramusage").ToCharArray().Where(Char.IsDigit)); // MVP! WHY SOMEONE CHOSE SWITCH FOR THIS SOLUTION..? :(
            summ.RamUsage = int.Parse(cancer);

            return summ;
        }

        public override List<string> GetApplicationList()
        {
            List<string> list = new List<string>();
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
            foreach (ManagementObject mo in mos.Get())
            {
  
                list.Add(mo["Name"].ToString());

            }

            return list;
        }

        public override List<string> GetHardwareList()
        {
            List<string> hardwareList = new List<string>();
            var searcher = new ManagementObjectSearcher("select * from win32_pnpentity");

            foreach (var obj in searcher.Get().Cast<ManagementObject>() )
            {
                
                hardwareList.Add(obj["Name"]?.ToString());

            }

            return hardwareList;
        }
    }
}
