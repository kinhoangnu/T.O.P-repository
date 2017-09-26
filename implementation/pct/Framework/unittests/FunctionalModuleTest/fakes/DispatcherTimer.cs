using System.Resources;

namespace System.Windows.Threading
{
    public class DispatcherTimer
    {
        private static DispatcherTimer _instance;
        public TimeSpan Interval;

        public event EventHandler Tick;

        public static void Reset()
        {
            _instance = null;
        }

        public static void Kick()
        {
            if ((_instance != null) && (_instance.Tick != null))
            {
                _instance.Tick(null, null);
            }
        }


        public DispatcherTimer()
        {
            _instance = this;
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }


    }
}
