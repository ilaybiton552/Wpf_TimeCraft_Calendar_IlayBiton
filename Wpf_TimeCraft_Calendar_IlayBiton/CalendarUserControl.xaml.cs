using System;
using System.Collections.Generic;
using System.Globalization;
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
using Calendar = Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference.Calendar;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for CalendarUserControl.xaml
    /// </summary>
    public partial class CalendarUserControl : UserControl
    {
        private Color baseColor;
        private DateTime currDisplayMonth;
        private Calendar calendar;
        private CalendarServiceClient serviceClient;
        public CalendarUserControl(ref Calendar calendar)
        {
            InitializeComponent();
            this.calendar = calendar;
            serviceClient = new CalendarServiceClient();
            baseColor = calendar.BaseColor;
            currDisplayMonth = DateTime.Now;
            prevBtn.Background = nextBtn.Background = new SolidColorBrush(baseColor);
            LoadDays();   
        }

        private void LoadDays()
        {
            currMonth.Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(currDisplayMonth.Month) + ' ' + currDisplayMonth.Year.ToString();
            int numOfDays = DateTime.DaysInMonth(currDisplayMonth.Year, currDisplayMonth.Month);
            int startOfMonth = (int)new DateTime(currDisplayMonth.Year, currDisplayMonth.Month, 1).DayOfWeek;
            int numOfDaysPrevious = new DateTime(currDisplayMonth.Year, currDisplayMonth.Month, 1).AddDays(-1).Day;
            int startDayPreviousMonth = numOfDaysPrevious - startOfMonth + 1;
            int lastDayNextMonth = 42 - numOfDays - startOfMonth;
            for (int i = 0; i < startOfMonth; i++)
            {
                AddControl(startDayPreviousMonth + i, GetEvents(new DateTime(currDisplayMonth.Year, currDisplayMonth.Month, 1).AddMonths(-1).AddDays(i + startDayPreviousMonth - 1)), 0.5);
            }
            for (int i = 1; i <= numOfDays; i++)
            {
                AddControl(i, GetEvents(new DateTime(currDisplayMonth.Year, currDisplayMonth.Month, i)));
            }
            for (int i = 1; i <= lastDayNextMonth; i++)
            {
                AddControl(i, GetEvents(new DateTime(currDisplayMonth.Year, currDisplayMonth.Month, i).AddMonths(1)), 0.5);
            }
        }

        private EventList GetEvents(DateTime day)
        {
            EventList events = new EventList();
            foreach (Event _event in calendar.Events) 
            {
                if (_event.StartDate.Date <= day.Date && _event.DueDate.Date >= day.Date)
                {
                    events.Add(_event);
                }
            }
            return events;
        }

        private void AddControl(int day, EventList events, double opacity = 1)
        {
            CalendarDayUserControl dayUC = new CalendarDayUserControl(day, events, baseColor);
            dayUC.Width = 100;
            dayUC.Height = 65;
            dayUC.Margin = new Thickness(0.5);
            dayUC.Opacity = opacity;
            Containter.Children.Add(dayUC);
        }

        private void prevBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            calendar.Events = serviceClient.GetCalendarEvents(calendar);
            currDisplayMonth = currDisplayMonth.AddMonths(-1);
            Containter.Children.Clear();
            LoadDays();
        }

        private void nextBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            calendar.Events = serviceClient.GetCalendarEvents(calendar);
            currDisplayMonth = currDisplayMonth.AddMonths(1);
            Containter.Children.Clear();
            LoadDays();
        }
    }
}
