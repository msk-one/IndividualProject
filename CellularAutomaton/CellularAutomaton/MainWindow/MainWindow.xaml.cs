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
using System.Windows.Shapes;
using CellularAutomaton.Other;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.ComponentModel;

namespace CellularAutomaton.MainWindow
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string gridSize {
            get;
            set;
        }
        public string gridMaxSize { get; set; }
        public Point StartPoint { get; set; }
        public Point StartPoint2 { get; set; }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public MainWindow()
        {
            gridSize = "0,0,10,10";
            gridMaxSize = "0,0,1024,1024";
            StartPoint = new Point(10, 0);
            StartPoint2 = new Point(0, 10);

            InitializeComponent();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                  new PropertyChangedEventArgs(propertyName));
            }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Other.AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit?", MessageBoxButton.OKCancel,
                MessageBoxImage.Question, MessageBoxResult.Cancel, MessageBoxOptions.None) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            } 
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog(this);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            RuleSettingsEditor.RuleSettingsEditor ruleSettings = new RuleSettingsEditor.RuleSettingsEditor();
            ruleSettings.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //TODO: fix it to use WPF scale

            //int currentWidth = Convert.ToInt32(StartPoint.X);
            //int currentHeight = Convert.ToInt32(StartPoint2.Y);

            //if (currentWidth < 120 && currentWidth >= 10)
            //{
            //    gridSize = "0,0," + (currentWidth + 10) + "," + (currentHeight + 10);
            //    NotifyPropertyChanged("gridSize");
            //    StartPoint = new Point(currentWidth + 10, 0);
            //    NotifyPropertyChanged("StartPoint");
            //    StartPoint2 = new Point(0, currentHeight + 10);
            //    NotifyPropertyChanged("StartPoint2");

            //    foreach (Rectangle rect in mainCanvas.Children)
            //    {
            //        rect.Width = currentWidth + 10;
            //        rect.Height = currentHeight + 10;
            //        Tuple<int, int> snappedCoords = snapToGrid(Canvas.GetLeft(rect) + 1, Canvas.GetTop(rect) + 1, currentWidth + 10);
            //        Canvas.SetTop(rect, snappedCoords.Item2);
            //        Canvas.SetLeft(rect, snappedCoords.Item1);
            //    }
            //}
            //else if (currentWidth < 10 && currentWidth >= 4)
            //{
            //    gridSize = "0,0," + (currentWidth + 1) + "," + (currentHeight + 1);
            //    NotifyPropertyChanged("gridSize");
            //    StartPoint = new Point(currentWidth + 1, 0);
            //    NotifyPropertyChanged("StartPoint");
            //    StartPoint2 = new Point(0, currentHeight + 1);
            //    NotifyPropertyChanged("StartPoint2");

            //    foreach (Rectangle rect in mainCanvas.Children)
            //    {
            //        rect.Width = currentWidth + 1;
            //        rect.Height = currentHeight + 1;
            //        Tuple<int, int> snappedCoords = snapToGrid(Canvas.GetLeft(rect)+1, Canvas.GetTop(rect) + 1, currentWidth + 1);
            //        Canvas.SetTop(rect, snappedCoords.Item2);
            //        Canvas.SetLeft(rect, snappedCoords.Item1);
            //    }
            //}
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            //TODO: fix it to use WPF scale

            //int currentWidth = Convert.ToInt32(StartPoint.X);
            //int currentHeight = Convert.ToInt32(StartPoint2.Y);

            //if (currentWidth > 10)
            //{
            //    gridSize = "0,0," + (currentWidth - 10) + "," + (currentHeight - 10);
            //    NotifyPropertyChanged("gridSize");
            //    StartPoint = new Point(currentWidth - 10, 0);
            //    NotifyPropertyChanged("StartPoint");
            //    StartPoint2 = new Point(0, currentHeight - 10);
            //    NotifyPropertyChanged("StartPoint2");

            //    foreach (Rectangle rect in mainCanvas.Children)
            //    {
            //        rect.Width = currentWidth - 10;
            //        rect.Height = currentHeight - 10;
            //        Tuple<int, int> snappedCoords = snapToGrid(Canvas.GetLeft(rect) + 1, Canvas.GetTop(rect) + 1, currentWidth - 10);
            //        Canvas.SetTop(rect, snappedCoords.Item2);
            //        Canvas.SetLeft(rect, snappedCoords.Item1);
            //    }
            //}
            //else if(currentWidth < 11 && currentWidth > 4)
            //{
            //    gridSize = "0,0," + (currentWidth - 1) + "," + (currentHeight - 1);
            //    NotifyPropertyChanged("gridSize");
            //    StartPoint = new Point(currentWidth - 1, 0);
            //    NotifyPropertyChanged("StartPoint");
            //    StartPoint2 = new Point(0, currentHeight - 1);
            //    NotifyPropertyChanged("StartPoint2");

            //    foreach (Rectangle rect in mainCanvas.Children)
            //    {
            //        rect.Width = currentWidth - 1;
            //        rect.Height = currentHeight - 1;
            //        Tuple<int, int> snappedCoords = snapToGrid(Canvas.GetLeft(rect) + 1, Canvas.GetTop(rect) + 1, currentWidth - 1);
            //        Canvas.SetTop(rect, snappedCoords.Item2);
            //        Canvas.SetLeft(rect, snappedCoords.Item1);
            //    }
            //}
        }

        private Tuple<int, int> snapToGrid(double x, double y, int snapSize)
        {
            int toSnapX = Convert.ToInt32(x);
            int toSnapY = Convert.ToInt32(y);

            int snappedX = toSnapX - (toSnapX % snapSize);
            int snappedY = toSnapY - (toSnapY % snapSize);

            return new Tuple<int, int>(snappedX+1, snappedY+1);
        }

        private void mainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int size = Convert.ToInt32(StartPoint.X);
            
            Rectangle rect = new Rectangle();

            rect.Width = size-1;
            rect.Height = size-1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y, size);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            mainCanvas.Children.Add(rect);
        }

        private void mainCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            int size = Convert.ToInt32(StartPoint.X);

            Rectangle rect = new Rectangle();

            rect.Width = size - 1;
            rect.Height = size - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y, size);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            mainCanvas.Children.Add(rect);
        }
    }
}