using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
        private string root = @"root\CIMV2";

        public virtual string GetMetric(ComputerMetricsEnum.ComputerMetrics metric)
        {
            var value = "";
            int i = (int) metric;
            switch (i)
            {
                case 1:
                    value = Environment.MachineName;
                    break;
                case 2:
                    value = Environment.UserName;
                    break;
                case 3:
                    var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                    foreach (var obj in searcher.Get().Cast<ManagementObject>())
                    {
                        value = obj["PercentProcessorTime"].ToString();
                        break;
                    }
                    break;
                case 4:
                    ManagementObjectSearcher searcher2 =new ManagementObjectSearcher(root,"SELECT * FROM Win32_OperatingSystem");

                    foreach (ManagementObject queryObj in searcher2.Get())
                    {
                        double free = Double.Parse(queryObj["FreePhysicalMemory"].ToString());
                        double total = Double.Parse(queryObj["TotalVisibleMemorySize"].ToString());
                        var ramUsage = (int)Math.Round((total - free) / total * 100, 2);
                        value = ramUsage.ToString();
                    }
                    break;
                default:
                    value = "";
                    break;
            }
            return value;
        }

        public abstract ComputerSummary GetComputerSummary();

    }
}
