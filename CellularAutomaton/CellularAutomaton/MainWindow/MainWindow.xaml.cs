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

        public Grid mainGrid;
        public Simulation mainSim;
        State EMPTY;
        State ALIVE;
        State DEAD;

        public int sizeOfCell { get; set; }
        public int scaleOfGrid { get; set; }
        public int gridMaxSize { get; set; }

        public Point StartPoint { get; set; }
        public Point StartPoint2 { get; set; }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public MainWindow()
        {
            mainGrid = new Grid();
            mainSim = new Simulation();

            EMPTY = new State();
            EMPTY.name = "EMPTY";
            EMPTY.type = 0;

            ALIVE = new State();
            ALIVE.name = "ALIVE";
            ALIVE.type = 1;

            DEAD = new State();
            DEAD.name = "DEAD";
            DEAD.type = 2;

            this.sizeOfCell = 10;
            this.gridMaxSize = 1024;
            this.scaleOfGrid = 1;
            StartPoint = new Point(0, 10);
            StartPoint2 = new Point(10, 0);

            mainGrid.cells = new List<Cell>(1024*1024);
            for (int i = 0; i <= 1024; i++)
            {
                for (int j = 0; j <= 1024; j++)
                {
                    Cell tmpCell = new Cell()
                    {
                        x = i,
                        y = j,
                        currentState = EMPTY,
                        Grid = mainGrid
                    };

                    mainGrid.cells.Add(tmpCell);
                }
            }

            mainSim.gridCanvas = mainCanvas;
            mainGrid.Simulation = mainSim;
            mainSim.grid = mainGrid;

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
            if (scaleOfGrid < 15)
            {
                scaleOfGrid += 1;
                NotifyPropertyChanged("scaleOfGrid");
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (scaleOfGrid > 1)
            {
                scaleOfGrid -= 1;
                NotifyPropertyChanged("scaleOfGrid");
            }
        }

        private Tuple<int, int> getCellCoordsfromCanvas(int x, int y)
        {
            int cX = x - (x % this.sizeOfCell);
            int cY = y - (y % this.sizeOfCell);

            cX /= 10;
            cY /= 10;

            return new Tuple<int, int>(cX, cY);
        }

        private Tuple<int, int> getCanvasCoordsFromCell(Cell cell)
        {
            int cvX = cell.x * 10;
            int cvY = cell.y * 10;

            return new Tuple<int, int>(cvX+1, cvY+1);
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
            Rectangle rect = new Rectangle();

            rect.Width = this.sizeOfCell - 1;
            rect.Height = this.sizeOfCell - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y, this.sizeOfCell);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            mainCanvas.Children.Add(rect);

            Cell curCell = mainGrid.cells.Find(c => c.x == getCellCoordsfromCanvas((int)e.GetPosition(mainCanvas).X, (int)e.GetPosition(mainCanvas).Y).Item1 && c.y == getCellCoordsfromCanvas((int)e.GetPosition(mainCanvas).X, (int)e.GetPosition(mainCanvas).Y).Item2);
            curCell.currentState = ALIVE;
        }

        private void mainCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = new Rectangle();

            rect.Width = this.sizeOfCell - 1;
            rect.Height = this.sizeOfCell - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y, this.sizeOfCell);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            mainCanvas.Children.Add(rect);

            Cell curCell = mainGrid.cells.Find(c => c.x == getCellCoordsfromCanvas((int)e.GetPosition(mainCanvas).X, (int)e.GetPosition(mainCanvas).Y).Item1 && c.y == getCellCoordsfromCanvas((int)e.GetPosition(mainCanvas).X, (int)e.GetPosition(mainCanvas).Y).Item2);
            curCell.currentState = EMPTY;
        }
    }
}