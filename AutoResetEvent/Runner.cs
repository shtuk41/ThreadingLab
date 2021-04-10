using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEvent
{
    public class Runner
    {

        #region fields

        private System.Timers.Timer timer;
        private static AutoResetEventExpire mre = new AutoResetEventExpire(true, 15000);

        #endregion

        #region constructor

        public Runner()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 2000;
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
            mre.WaitOne(false);
            Console.WriteLine("OnTimeEvent() Mutex acquired");
            Thread.Sleep(2000);
            Console.WriteLine("OnTimeEvent() Releasing mutex");
            mre.Set();
            Console.WriteLine("OnTimeEvent() released mutex");
            timer.Start();
        }

        #endregion

        #region methods

        public void Start()
        {
            Console.WriteLine("started");
            mre.WaitOne(true);
            Console.WriteLine("Start() Mutex acquired");
        }

        public void Stop()
        {
            Console.WriteLine("Begin Stop()");
            mre.Set();
            Console.WriteLine("Stop() mutex released");

            Console.WriteLine("stopped");
        }

        #endregion

    }
}
