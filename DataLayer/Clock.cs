using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Clock
    {

        private int interval;

        public delegate void SecondChangeHandler(
                   object clock, DataEventArgs dataInformation);

        public event SecondChangeHandler SecondChange;

        public Clock()
        {
            Console.WriteLine("Enter input second:"); // Prompt
            string inter = Console.ReadLine();
            interval = int.Parse(inter)*1000;
        }

        //Fires the event
        protected void OnSecondChange(
         object clock,
         DataEventArgs dataInformation)
        {
            // Check if there are any Subscribers
            if (SecondChange != null)
            {
                // Call the Event
                SecondChange(clock, dataInformation);
            }
        }

        public void Run()
        {
            for (;;)
            {
                // Sleep 1 Second
                Thread.Sleep(interval);

                // Get the current time
                DateTime dt = DateTime.Now;
                DataManager dataManager = new FullDataManager();

                // If the second has changed
                // notify the subscribers
                /* if (dt.Second != _second)
                 {*/
                var computerSummary = dataManager.GetComputerSummary();
                // Create the EventArgs object
                // to pass to the subscribers
                DataEventArgs dataInformation =
                   new DataEventArgs(dt, computerSummary.CpuUsage, computerSummary.RamUsage);

                // If anyone has subscribed, notify them
                OnSecondChange(this, dataInformation);
                //                }


            }
        }
    }

}
