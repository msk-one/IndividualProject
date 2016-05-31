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

namespace CellularAutomaton.RuleSettingsEditor
{
    public partial class RuleSettingsEditor : Window
    {
        Neighborhood currNeigh;
        Rule currRule;
        RuleSet currSet;

        public RuleSettingsEditor()
        {
            InitializeComponent();
            currNeigh = new Neighborhood();
            currNeigh.size = 5;
            currNeigh.cells = new List<Cell>(5*5);
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    Cell tmpCell = new Cell()
                    {
                        x = i,
                        y = j,
                        currentState = State.Empty,
                        Grid = null
                    };

                    currNeigh.cells.Add(tmpCell);
                }
            }

            Cell mainCell = currNeigh.cells.Find(c => c.x == 2 && c.y == 2);
            mainCell.currentState = State.Alive;

            Rectangle rect = new Rectangle();

            rect.Width = 30 - 1;
            rect.Height = 30 - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(getCanvasCoordsFromCell(mainCell).Item1, getCanvasCoordsFromCell(mainCell).Item2, 30);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            ruleCanvas.Children.Add(rect);
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private Tuple<int, int> getCellCoordsfromCanvas(int x, int y)
        {
            int cX = x - (x % 30);
            int cY = y - (y % 30);

            cX /= 30;
            cY /= 30;

            return new Tuple<int, int>(cX+1, cY+1);
        }

        private Tuple<int, int> getCanvasCoordsFromCell(Cell cell)
        {
            int cvX = cell.x * 30;
            int cvY = cell.y * 30;

            return new Tuple<int, int>(cvX, cvY);
        }

        private Tuple<int, int> snapToGrid(double x, double y, int snapSize)
        {
            int toSnapX = Convert.ToInt32(x);
            int toSnapY = Convert.ToInt32(y);

            int snappedX = toSnapX - (toSnapX % snapSize);
            int snappedY = toSnapY - (toSnapY % snapSize);

            return new Tuple<int, int>(snappedX+1, snappedY+1);
        }

        private void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = new Rectangle();

            rect.Width = 30 - 1;
            rect.Height = 30 - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(ruleCanvas).X, e.GetPosition(ruleCanvas).Y, 30);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            ruleCanvas.Children.Add(rect);
            Cell curCell = currNeigh.cells.Find(c => c.x == getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y).Item1 && c.y == getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y).Item2);
            curCell.currentState = State.Empty;
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = new Rectangle();

            rect.Width = 30 - 1;
            rect.Height = 30 - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(ruleCanvas).X, e.GetPosition(ruleCanvas).Y, 30);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            ruleCanvas.Children.Add(rect);
            Cell curCell = currNeigh.cells.Find(c => c.x == getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y).Item1 && c.y == getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y).Item2);
            curCell.currentState = State.Alive;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            ruleCanvas.Children.Clear();
        }
    }
}
