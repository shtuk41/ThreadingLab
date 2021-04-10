using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadingLab
{
    public class Runner
    {

        #region fields

        private System.Timers.Timer timer;
        private static Mutex mut = new Mutex();

        #endregion

        #region constructor
        
        public Runner()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = false;
            timer.Enabled = true;
        }

        #endregion

        #region events

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            Console.WriteLine("Raised: {0}", e.SignalTime);
            mut.WaitOne();
            Console.WriteLine("OnTimeEvent() Mutex acquired");
            Thread.Sleep(2000);
            Console.WriteLine("OnTimeEvent() Releasing mutex");
            mut.ReleaseMutex();
            Console.WriteLine("OnTimeEvent() released mutex");
            timer.Start();
        }

        #endregion

        #region methods

        public void Start()
        {
            Console.WriteLine("started");
            mut.WaitOne();
            Console.WriteLine("Start() Mutex acquired");
        }

        public void Stop()
        {
            Console.WriteLine("Begin Stop()");
            mut.ReleaseMutex();
            Console.WriteLine("Stop() mutex released");

            Console.WriteLine("stopped");
        }

        #endregion

    }
}
