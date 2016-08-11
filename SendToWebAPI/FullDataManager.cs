using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Threading;

namespace DataLayer
{
    public class FullDataManager : DataManager
    {
        public override UsageData GetComputerSummary()
        {
            UsageData summ = new UsageData();

            summ.Name = GetMetric("computername");
            summ.processorUsage = int.Parse(GetMetric("cpuusage"));
            string cancer = string.Join("", GetMetric("ramusage").ToCharArray().Where(Char.IsDigit)); 
            summ.memoryUsage = int.Parse(cancer);

            return summ;

          
        }

    }

}
