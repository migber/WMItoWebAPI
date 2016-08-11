using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataEventArgs: EventArgs
    {
        public DataEventArgs(DateTime time, int cpuUsage, int ramUsage)
        {

            Time = time;
            CpuUsage = cpuUsage;
            RamUsage = ramUsage;
           
        }

        public  DateTime Time { get; set; }
        public readonly int CpuUsage;
        public readonly int RamUsage;
    }
}
