using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Newtonsoft.Json;
using SendToWebAPI;

namespace MB_Final_1
{
    class Program
    {
        private DataManager dataManager;

        static void Main(string[] args)
        {
            var dataManager = new FullDataManager();
           /* var computername = dataManager.GetComputerSummary().Name;
            Console.WriteLine($"Computer Name: {computername}");

            var User = dataManager.GetComputerSummary().User;
            Console.WriteLine($"User Name: {User}");
            */
            Clock clock = new Clock();
            clock.SecondChange += ClockOnSecondChange;
            clock.Run();


        }

        private static void ClockOnSecondChange(object clock, DataEventArgs dataInformation)
        {

            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri("http://192.168.10.106/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var newUsageReport = new NewUsageData();
                newUsageReport.MemoryUsage = dataInformation.RamUsage;
                newUsageReport.ProcessorUsage = dataInformation.CpuUsage;
                newUsageReport.TimeStamp = dataInformation.Time;

                var json = JsonConvert.SerializeObject(newUsageReport);

                var content = new StringContent(json);

                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                var response = client.PostAsync("/api/virtualmachines/7/usagereports", content);

                var result = response.Result;
            }



            var CpuUsage = dataInformation.CpuUsage;
            Console.WriteLine($"CPU usage: {CpuUsage}%");

            var RamUsage = dataInformation.RamUsage;
            Console.WriteLine($"RAM usage: {RamUsage}%");

            var time = dataInformation.Time.ToString("hh:mm:ss");
            Console.WriteLine($"Time: {time}");


        }
    }
}
