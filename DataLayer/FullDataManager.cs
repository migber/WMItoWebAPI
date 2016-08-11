using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public class FullDataManager : DataManager
    {
        public override ComputerSummary GetComputerSummary()
        {
            var computerSummary = new ComputerSummary
            {
                Name = GetMetric(ComputerMetricsEnum.ComputerMetrics.ComputerName),
                User = GetMetric(ComputerMetricsEnum.ComputerMetrics.User),
                CpuUsage = int.Parse(GetMetric(ComputerMetricsEnum.ComputerMetrics.CpuUsage)),
                RamUsage = int.Parse(GetMetric(ComputerMetricsEnum.ComputerMetrics.RamUsage))
            };

            return computerSummary;
        }
    }
}
