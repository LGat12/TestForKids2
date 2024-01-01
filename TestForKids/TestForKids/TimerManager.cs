using System;
using System.Windows.Threading;


    internal class TimerManager
    {
        private DispatcherTimer timer;

        public TimerManager(EventHandler timerTickHandler)
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += timerTickHandler;
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public void ResetTimer()
        {
            timer.Stop();
            // Other logic for resetting the timer...
        }
    }

