namespace com.vanderlande.wpf
{
    public class WindowViewModel
    {
        protected WindowViewModel Window;

        protected WindowViewModel()
        {
            Window = this;
        }


        protected bool ChangeProperty(ref int oldVal, int newVal)
        {
            if (oldVal == newVal)
            {
                return false;
            }
            oldVal = newVal;
            return true;
        }

        public virtual void OnCreated()
        {
        }

        public virtual void Close()
        {
        }
    }
}
