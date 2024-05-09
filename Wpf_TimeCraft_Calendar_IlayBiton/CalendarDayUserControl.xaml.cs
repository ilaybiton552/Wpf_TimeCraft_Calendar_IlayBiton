using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for CalendarDayUserControl.xaml
    /// </summary>
    public partial class CalendarDayUserControl : UserControl
    {
        public CalendarDayUserControl(int day, EventList events, Color color)
        {
            InitializeComponent();
            dayOfMonth.Text = day.ToString();
            textBorder.Background = new SolidColorBrush(color);
            textBorder.Background.Opacity = 0.5;
            foreach (Event _event in events) 
            {
                AddEvent(_event);
            }
        }

        private void AddEvent(Event _event)
        {
            Button button = new Button();
            button.Tag = _event;
            button.Content = _event.EventName;
            button.Style = FindResource("eventButton") as Style;
            button.Background = new SolidColorBrush(_event.EventBackground);
            button.Background.Opacity = 0.75;
            button.Click += Event_Click;
            Events.Children.Add(button);
        }

        private void Event_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MessageBox.Show((button.Tag as Event).EventName);
        }

    }
}
