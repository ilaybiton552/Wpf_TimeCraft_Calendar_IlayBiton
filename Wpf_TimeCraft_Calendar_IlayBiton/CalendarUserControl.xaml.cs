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
        public CalendarUserControl(Calendar calendar)
        {
            InitializeComponent();
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
                AddControl(startDayPreviousMonth + i, 0.5);
            }
            for (int i = 1; i <= numOfDays; i++)
            {
                AddControl(i);
            }
            for (int i = 1; i <= lastDayNextMonth; i++)
            {
                AddControl(i, 0.5);
            }
        }

        private void AddControl(int day, double opacity = 1)
        {
            CalendarDayUserControl dayUC = new CalendarDayUserControl(day, baseColor);
            dayUC.Width = 100;
            dayUC.Height = 65;
            dayUC.Margin = new Thickness(0.5);
            dayUC.Opacity = opacity;
            Containter.Children.Add(dayUC);
        }

        private void prevBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currDisplayMonth = currDisplayMonth.AddMonths(-1);
            Containter.Children.Clear();
            LoadDays();
        }

        private void nextBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currDisplayMonth = currDisplayMonth.AddMonths(1);
            Containter.Children.Clear();
            LoadDays();
        }
    }
}
