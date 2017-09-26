namespace com.vanderlande.wpf
{
    public static class Logger
    {
        private static string _function;

        public static string TheFunction
        {
            get
            {
                string retval = _function;
                _function = string.Empty;
                return retval;
            }
            private set { _function = value; }
        }

        public static void LogError(string str)
        {
            string[] arr = str.Split(' ');
            TheFunction = arr[0];
        }
    }
}
