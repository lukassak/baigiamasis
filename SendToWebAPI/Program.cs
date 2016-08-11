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

namespace SendToWebAPI
{
    class Program
    {
        private static UsageData _data;
        private static FullDataManager _sum;

        static void Main()
        {
            
            RunAsync().Wait();

        }

        static async Task RunAsync()
        {

            while (true)
            {
                using (var client = new HttpClient())
                {
                    _data = new UsageData();
                    _sum = new FullDataManager();
                    _data = _sum.GetComputerSummary();

                    client.BaseAddress = new Uri("http://192.168.10.106/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var newUsageReport = new UsageData
                    {
                        TimeStamp = DateTime.Now.AddHours(-2),
                        processorUsage = _data.processorUsage,
                        memoryUsage = _data.memoryUsage
                    };
                    var json = JsonConvert.SerializeObject(newUsageReport);

                    var content = new StringContent(json);

                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                    var response = client.PostAsync("api/virtualmachines/12/usagereports", content);
                    try
                    {
                        var result = response.Result;
                    }
                    catch (Exception ex)
                    {
                        var me = ex.Message;
                    }
                    Console.WriteLine("Pass");
                }

                Thread.Sleep(4000);
            }
        }
    }
}
