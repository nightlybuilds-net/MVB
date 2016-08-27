using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Mvb.FakeContacts.Wpf.App
{
    /// <summary>
    /// Interaction logic for ContactRow.xaml
    /// </summary>
    public partial class ContactRow : StackPanel
    {
        public ContactRow(string contactName)
        {
            this.InitializeComponent();
            this.ContactName.Content = contactName;
        }
        
    }
}
