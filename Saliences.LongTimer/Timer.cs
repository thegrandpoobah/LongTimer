using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime;
using System.Timers;

namespace Saliences.LongTimer
{
    [DefaultEvent("Elapsed")]
    [DefaultProperty("Interval")]
    [ToolboxBitmap(typeof(Saliences.LongTimer.Timer), "Control_Timer.bmp")]
    public class Timer : Component, ISupportInitialize
    {
        private const double MaxInterval = Int32.MaxValue;

        private System.Timers.Timer internalTimer;

        public Timer()
            : this(100)
        {
        }

        public Timer(double interval)
        {
            this.internalTimer = new System.Timers.Timer();
            this.internalTimer.Elapsed += new ElapsedEventHandler(onInternalTimerElapsed);
            this.Interval = interval;
        }

        [Category("Behavior")]
        [TimersDescription("TimerAutoReset")]
        [DefaultValue(true)]
        public bool AutoReset
        {
            get
            {
                return this.internalTimer.AutoReset;
            }
            set
            {
                this.internalTimer.AutoReset = value;
            }
        }

        [DefaultValue(false)]
        [Category("Behavior")]
        [TimersDescription("TimerEnabled")]
        public bool Enabled
        {
            get
            {
                return this.internalTimer.Enabled;
            }
            set
            {
                this.internalTimer.Enabled = true;
            }
        }

        [DefaultValue(100)]
        [Category("Behavior")]
        [TimersDescription("TimerInterval")]
        [SettingsBindable(true)]
        public double Interval
        {
            get
            {
                return this.fullInterval;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                this.remainingInterval = this.fullInterval = value;
                this.UpdateInternalInterval();
            }
        }
        private double fullInterval;
        private double remainingInterval;

        public override ISite Site
        {
            get
            {
                return this.internalTimer.Site;
            }
            set
            {
                this.internalTimer.Site = value;
            }
        }

        [Browsable(false)]
        [DefaultValue("")]
        [TimersDescription("TimerSynchronizingObject")]
        public ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                return this.internalTimer.SynchronizingObject;
            }
            set
            {
                this.internalTimer.SynchronizingObject = value;
            }
        }

        [Category("Behavior")]
        [TimersDescription("TimerIntervalElapsed")]
        public event ElapsedEventHandler Elapsed;

        public void BeginInit()
        {
            this.internalTimer.BeginInit();
        }

        public void Close()
        {
            this.internalTimer.Close();
        }

        protected override void Dispose(bool disposing)
        {
            this.internalTimer.Dispose();
            base.Dispose(disposing);
        }

        public void EndInit()
        {
            this.internalTimer.EndInit();
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Start()
        {
            this.remainingInterval = this.fullInterval;
            this.internalTimer.Start();
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Stop()
        {
            this.internalTimer.Stop();
        }

        private void UpdateInternalInterval()
        {
            this.internalTimer.Interval = Math.Min(MaxInterval, this.remainingInterval);
        }

        private void onInternalTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.remainingInterval <= MaxInterval)
            {
                if (this.AutoReset)
                {
                    this.remainingInterval = this.fullInterval;
                    this.UpdateInternalInterval();
                }
                this.Elapsed(this, e);
            }
            else
            {
                this.remainingInterval -= MaxInterval;
                this.UpdateInternalInterval();
            }
        }
    }
}
