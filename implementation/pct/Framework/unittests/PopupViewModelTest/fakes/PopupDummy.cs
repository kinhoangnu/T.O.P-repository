namespace com.vanderlande.wpf
{
    public class PopupDummy : PopupViewModel
    {
        public bool IsOpen;

        public override void OnCreated()
        {
            base.OnCreated();
            IsOpen = true;
        }

        public override void Close()
        {
            IsOpen = false;
        }
    }
}
