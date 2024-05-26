using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for BaseColorListUserControl.xaml
    /// </summary>
    public partial class BaseColorListUserControl : UserControl
    {
        private List<string> colors;
        private string chosenColor;
        public Color ChosenColor 
        { 
            get 
            {
                BrushConverter bc = new BrushConverter();
                return ((SolidColorBrush)bc.ConvertFrom(chosenColor)).Color; 
            } 
        }
        public BaseColorListUserControl()
        {
            InitializeComponent();
            colors = new List<string>() { "#ff1919", "#19ff19", "#1919ff", "#de32de", "#dede32", "#32dede", "#732396", "#ff7819", "#197819" };
            AddColors();
        }
        public void SetColor(Color color)
        {
            innerBorder.Background = new SolidColorBrush(color);
        }
        private Border CreateColorDisplay(string color)
        {
            Border outBorder = new Border()
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1.5),
                Margin = new Thickness(0.5),
                Width = 20,
                Height = 20
            };
            Border inBorder = new Border()
            {
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(0.5)
            };
            BrushConverter brushConverter = new BrushConverter();
            outBorder.Child = inBorder;
            outBorder.Tag = color;
            outBorder.MouseLeftButtonDown += OutBorder_MouseLeftButtonDown;
            inBorder.Background = brushConverter.ConvertFrom(color) as SolidColorBrush;
            return outBorder;
        }
        private void OutBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chosenColor = (sender as Border).Tag as string;
            popup.IsOpen = false;
            BrushConverter brushConverter = new BrushConverter();
            innerBorder.Background = brushConverter.ConvertFrom(chosenColor) as SolidColorBrush;
        }
        private void AddColors()
        {
            foreach (string color in colors)
            {
                colorsWP.Children.Add(CreateColorDisplay(color));
            }
        }
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            popup.IsOpen = true;
        }
        private void popup_MouseLeave(object sender, MouseEventArgs e)
        {
            popup.IsOpen = false;
        }
    }
}
