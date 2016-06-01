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


        public int sizeOfCell { get; set; }
        public int scaleOfGrid { get; set; }
        public int gridMaxSize { get; set; }

        public Point StartPoint { get; set; }
        public Point StartPoint2 { get; set; }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private bool isMoving = false;                  //False - ignore mouse movements and don't scroll
        private bool isDeferredMovingStarted = false;   //True - Mouse down -> Mouse up without moving -> Move; False - Mouse down -> Move
        private Point? startPosition = null;
        private double slowdown = 200;

        public MainWindow()
        {
            mainGrid = new Grid();
            mainSim = new Simulation();

            this.sizeOfCell = 10;
            this.gridMaxSize = 1920;
            this.scaleOfGrid = 1;
            StartPoint = new Point(0, 10);
            StartPoint2 = new Point(10, 0);

            mainGrid.cells = new List<Cell>(1920*1080);
            for (int i = 0; i <= 1920; i++)
            {
                for (int j = 0; j <= 1080; j++)
                {
                    Cell tmpCell = new Cell()
                    {
                        x = i,
                        y = j,
                        currentState = State.Empty,
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
            mainSim.stop();
            mainCanvas.Children.Clear();
            mainGrid.clear();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            mainSim.stop();
            mainCanvas.Children.Clear();
            mainGrid.clear();
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
            curCell.currentState = State.Alive;
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
            curCell.currentState = State.Empty;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mainSim.play();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainSim.pause();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            mainSim.stop();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            mainSim.pause();
            mainSim.play();
        }

        private void scrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            var sv = sender as ScrollViewer;

            if (this.isMoving && sv != null)
            {
                this.isDeferredMovingStarted = false; //standard scrolling (Mouse down -> Move)

                var currentPosition = e.GetPosition(sv);
                var offset = currentPosition - startPosition.Value;
                offset.Y /= slowdown;
                offset.X /= slowdown;

                //if(Math.Abs(offset.Y) > 25.0/slowdown)  //Some kind of a dead space, uncomment if it is neccessary
                sv.ScrollToVerticalOffset(sv.VerticalOffset + offset.Y);
                sv.ScrollToHorizontalOffset(sv.HorizontalOffset + offset.X);
            }
        }

        private void scrollViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        private void scrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {  
        }

        private void scrollViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.isMoving == true) //Moving with a released wheel and pressing a button
                this.CancelScrolling();
            else if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                if (this.isMoving == false) //Pressing a wheel the first time
                {
                    this.isMoving = true;
                    this.startPosition = e.GetPosition(sender as IInputElement);
                    this.isDeferredMovingStarted = true; //the default value is true until the opposite value is set                   
                }
            }
        }

        private void CancelScrolling()
        {
            this.isMoving = false;
            this.startPosition = null;
            this.isDeferredMovingStarted = false;
        }

        private void scrollViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Released && this.isDeferredMovingStarted != true)
                this.CancelScrolling();
        }
    }
}