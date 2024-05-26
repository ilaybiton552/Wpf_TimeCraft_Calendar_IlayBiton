using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private User user;
        public CalendarUserControl(ref Calendar calendar, ref User user)
        {
            InitializeComponent();
            this.calendar = calendar;
            this.user = user;
            baseColor = calendar.BaseColor;
            currDisplayMonth = DateTime.Now;
            prevBtn.Background = nextBtn.Background = new SolidColorBrush(baseColor);
            LoadDays();   
        }
        public void LoadDays()
        {
            Containter.Children.Clear();
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
            if (day.Month == user.Birthday.Month && day.Day == user.Birthday.Day)
            {
                events.Add(new Event() { ID = -1, EventName = "Happy Birthday!" });
            }
            if (calendar.Events != null)
            {
                foreach (Event _event in calendar.Events)
                {
                    if (_event.StartDate.Date <= day.Date && _event.DueDate.Date >= day.Date)
                    {
                        events.Add(_event);
                    }
                }
            }
            return events;
        }
        private void AddControl(int day, EventList events, double opacity = 1)
        {
            CalendarDayUserControl dayUC = new CalendarDayUserControl(day, events, baseColor, user);
            dayUC.Width = 100;
            dayUC.Height = 65;
            dayUC.Margin = new Thickness(0.5);
            dayUC.Opacity = opacity;
            Containter.Children.Add(dayUC);
        }
        private void prevBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currDisplayMonth = currDisplayMonth.AddMonths(-1);
            LoadDays();
        }
        private void nextBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currDisplayMonth = currDisplayMonth.AddMonths(1);
            LoadDays();
        }
    }
}
