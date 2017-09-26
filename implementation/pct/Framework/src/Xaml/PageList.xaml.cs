using System.Windows.Controls;
using System.Windows.Data;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList : UserControl
    {
        public PageList()
        {
            InitializeComponent();
        }

        private void OnFilterPages(object sender, FilterEventArgs e)
        {
            ContentEntry entry = e.Item as ContentEntry;
            e.Accepted = (entry == null) || (entry.Element != null);
        }
    }
}
