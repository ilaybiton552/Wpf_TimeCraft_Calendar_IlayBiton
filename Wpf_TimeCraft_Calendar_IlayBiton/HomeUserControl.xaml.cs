using System.Windows;
using System.Windows.Controls;
using System.Xml;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for HomeUserControl.xaml
    /// </summary>
    public partial class HomeUserControl : UserControl
    {
        private string hebrew, english;
        public HomeUserControl()
        {
            InitializeComponent();
            try
            {
                hebrew = GetDate("https://www.hebcal.com/etc/hdate-he.xml");
                english = GetDate("https://www.hebcal.com/etc/hdate-en.xml");
            }
            catch { }
            hebrewDate.Text = hebrew;
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            hebrewDate.Text = english;
        }
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            hebrewDate.Text = hebrew;
        }
        private string GetDate(string url)
        {
            XmlTextReader reader = new XmlTextReader(url);
            string date = "";
            while (reader.Read()) 
            {
                if (reader.Name == "description" && reader.NodeType == XmlNodeType.Element)
                {
                    reader.Read();
                    date = reader.Value;
                }
            }
            return date;
        }
    }
}
