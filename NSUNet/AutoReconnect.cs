#if !NSUWATCHER
using System;
using NSU.NSUSystem;
#if __ANDROID__
using System.Timers;
#elif WINDOWS_UWP
using Windows.System.Threading;
#endif

namespace NSU.Shared.NSUNet
{
    public class AutoReconnect
    {
        private const string LogTag = "AutoReconnect";
        public delegate void OnDoReconnectHandler(int count);

        public event OnDoReconnectHandler OnDoReconnect;

        bool working = false;
        int count;
        double currentInterval;


#if __ANDROID__
		Timer timer;
#else
        ThreadPoolTimer timer;
#endif

        public double FirstRetry { get; set; }
        public double Increment { get; set; }
        public double MaxInterval { get; set; }
        public bool IncrementalRetries { get; set; }
        public bool AutoReconnecting { get { return working; } }
        public double CurrentInterval { get { return currentInterval; } }
        public bool Enabled
        {
            get { return enabled; }
            set {
                enabled = value;
                NSULog.Debug(LogTag, $"Setting property Enabled = {enabled}");
                if (!enabled)
                    StopReconnect();
            }
        }
        private bool enabled = false;

        public AutoReconnect()
        {
            FirstRetry = 15000;
            currentInterval = FirstRetry;
            Increment = 15 * 1000;
            MaxInterval = 10 * 60 * 1000;
            count = 0;
            IncrementalRetries = true;
#if __ANDROID__
            timer = new Timer();
            timer.Interval = FirstRetry;
            timer.Elapsed += TimerElapsedHandler;
#endif
        }

        private void StartTimer()
        {
            working = false;
#if __ANDROID__
            timer.Enabled = false;
            timer.Interval = currentInterval;
            timer.Enabled = true;
#else
            try
            {
                if (timer != null)
                {
                    timer.Cancel();
                    timer = null;
                }
                timer = ThreadPoolTimer.CreateTimer(TimerElapsedHandler, TimeSpan.FromMilliseconds(currentInterval));
            }
            catch (Exception ex)
            {
                NSULog.Exception(LogTag, "StartTimer()" + ex.Message);
                StopTimer();
            }
#endif
            working = true;
        }

        private void StopTimer()
        {
#if __ANDROID__
            timer.Enabled = false;
#else
            try
            {
                if (timer != null)
                {
                    timer.Cancel();
                    timer = null;
                }
            }
            catch (Exception ex)
            {
                NSULog.Exception(LogTag, "StopReconnect()" + ex.Message);
            }
#endif
            working = false;
        }

        void Reset()
        {
            NSULog.Debug(LogTag, "Reset()");
            currentInterval = FirstRetry;
            count = 0;
        }


#if __ANDROID__
        void TimerElapsedHandler (object sender, ElapsedEventArgs e)
        {
#elif WINDOWS_UWP
        void TimerElapsedHandler(ThreadPoolTimer t)
        {
#endif
            NSULog.Debug("AutoReconnect", "HandleTimerElapsed()");
            StopTimer();
            if(IncrementalRetries)
            {
                currentInterval += Increment;
                if (currentInterval > MaxInterval)
                {
                    currentInterval = MaxInterval;
                }
            }            
            count++;
            OnDoReconnect?.Invoke(count);
        }

        public void StartReconnect()
        {
            if (enabled)
            {
                NSULog.Debug("AutoReconnect", $"Starting reconnect in {TimeSpan.FromMilliseconds(currentInterval).Minutes} min {TimeSpan.FromMilliseconds(currentInterval).Seconds} sec.");
                StartTimer();
            }
        }

        public void StopReconnect()
        {
            NSULog.Debug("AutoReconnect", "StopReconnect() called.");
            StopTimer();
            Reset();
        }
    }
}

#endif