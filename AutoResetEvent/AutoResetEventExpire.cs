using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEvent
{
    public class AutoResetEventExpire
    {
        #region fields

        private System.Threading.AutoResetEvent are;
        private System.Timers.Timer setTimer;
        private bool useExpireFlag = false;

        #endregion

        #region constructor

        public AutoResetEventExpire(bool initialState, int expireTime)
        {
            are = new System.Threading.AutoResetEvent(initialState);

            setTimer = new System.Timers.Timer();
            setTimer.Interval = expireTime;
            setTimer.Elapsed += OnTimedEvent;
            setTimer.AutoReset = false;
            setTimer.Enabled = true;
        }

        #endregion

        #region events
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (useExpireFlag)
            {
                are.Set();
            }

            setTimer.Stop();
            setTimer.Enabled = false;
        }

        #endregion

        #region methods

        public void WaitOne(bool useExpire)
        {
            are.WaitOne();
            useExpireFlag = useExpire;

            if (useExpire)
            {
                setTimer.Enabled = true;
                setTimer.Start();
            }
        }

        public void Set()
        {
            
            setTimer.Stop();
            setTimer.Enabled = false;

            are.Set();
        }

        #endregion
    }
}
