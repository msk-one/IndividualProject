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
            mainCanvas.Background = new SolidColorBrush(Color.FromRgb(255,255,255));
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            RuleSettingsEditor.RuleSettingsEditor ruleSettings = new RuleSettingsEditor.RuleSettingsEditor();
            ruleSettings.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            int currentWidth = Convert.ToInt32(StartPoint.X);
            int currentHeight = Convert.ToInt32(StartPoint2.Y);

            if (currentWidth < 120 && currentWidth >= 10)
            {
                gridSize = "0,0," + (currentWidth + 10) + "," + (currentHeight + 10);
                NotifyPropertyChanged("gridSize");
                StartPoint = new Point(currentWidth + 10, 0);
                NotifyPropertyChanged("StartPoint");
                StartPoint2 = new Point(0, currentHeight + 10);
                NotifyPropertyChanged("StartPoint2");
            }
            else if (currentWidth < 10 && currentWidth >= 3)
            {
                gridSize = "0,0," + (currentWidth + 1) + "," + (currentHeight + 1);
                NotifyPropertyChanged("gridSize");
                StartPoint = new Point(currentWidth + 1, 0);
                NotifyPropertyChanged("StartPoint");
                StartPoint2 = new Point(0, currentHeight + 1);
                NotifyPropertyChanged("StartPoint2");
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            int currentWidth = Convert.ToInt32(StartPoint.X);
            int currentHeight = Convert.ToInt32(StartPoint2.Y);

            if (currentWidth > 10)
            {
                gridSize = "0,0," + (currentWidth - 10) + "," + (currentHeight - 10);
                NotifyPropertyChanged("gridSize");
                StartPoint = new Point(currentWidth - 10, 0);
                NotifyPropertyChanged("StartPoint");
                StartPoint2 = new Point(0, currentHeight - 10);
                NotifyPropertyChanged("StartPoint2");
            }
            else if(currentWidth < 11 && currentWidth > 3)
            {
                gridSize = "0,0," + (currentWidth - 1) + "," + (currentHeight - 1);
                NotifyPropertyChanged("gridSize");
                StartPoint = new Point(currentWidth - 1, 0);
                NotifyPropertyChanged("StartPoint");
                StartPoint2 = new Point(0, currentHeight - 1);
                NotifyPropertyChanged("StartPoint2");
            }
        }
    }
}