#if !NSUWATCHER
using System;

#if __ANDROID__
using System.Timers;
#else
using Windows.System.Threading;
#endif

namespace NSU.Shared.NSUNet
{
    public class NSUTimer
    {
        public partial class Times { }

        public delegate void NSUTimerArgs();
        public event NSUTimerArgs OnNSUTimer;
#if __ANDROID__
        Timer timer;
#else
        ThreadPoolTimer timer = null;
        double intrvl;
        object lck;
        bool created = false;
#endif
        public NSUTimer(double interval)
        {
#if __ANDROID__
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += TimerElapsed;
#else
            intrvl = interval;
            #endif
        }

        public void Reset()
        {
#if __ANDROID__
            timer.Enabled = false;
            timer.Enabled = true;
#else
                timer?.Cancel();
                timer = null;
                timer = ThreadPoolTimer.CreatePeriodicTimer(timerElapsedHandler, TimeSpan.FromMilliseconds(intrvl));
#endif
        }

#if __ANDROID__
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnNSUTimer?.Invoke();
        }
#else
        void timerElapsedHandler(ThreadPoolTimer t)
        {
            OnNSUTimer?.Invoke();
        }
#endif

        public void Start()
        {
#if __ANDROID__
            timer.Enabled = true;
#else
            if(timer == null)
                Reset();
#endif
        }

        public void Stop()
        {
#if __ANDROID__
            timer.Enabled = false;
#else
            //lock (lck)
            //{
                timer?.Cancel();
                timer = null;
            //}
#endif
        }
    }
}

#endif