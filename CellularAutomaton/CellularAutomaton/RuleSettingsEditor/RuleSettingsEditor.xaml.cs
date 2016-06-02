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

        private int currCellCount;
        private State currInitState;
        private State currFinalState;

        private List<RuleSet> ruleSetList;

        public RuleSettingsEditor()
        {
            InitializeComponent();
            currNeigh = new Neighborhood();
            currNeigh.size = 5;

            ruleSetList = new List<RuleSet>();

            ruleSetList.Add(new RuleSet());
            ruleSetsListBox.Items.Add("Preset 1");
            ruleSetList.Add(new RuleSet());
            ruleSetsListBox.Items.Add("Preset 2");
            ruleSetList.Add(new RuleSet());
            ruleSetsListBox.Items.Add("Preset 3");
            ruleSetList.Add(new RuleSet());
            ruleSetsListBox.Items.Add("Preset 4");

            currNeigh.cells = new List<Cell>(5*5);
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
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

            Cell mainCell = currNeigh.cells.Find(c => c.x == 3 && c.y == 3);
            mainCell.currentState = State.Alive;

            Rectangle rect = new Rectangle();

            rect.Width = 30 - 1;
            rect.Height = 30 - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(getCanvasCoordsFromCell(mainCell).Item1-1, getCanvasCoordsFromCell(mainCell).Item2-1, 30);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            ruleCanvas.Children.Add(rect);

            currSet = new RuleSet();
            currSet.rules = new List<Rule>();
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
            Cell curCell =
                    currNeigh.cells.Find(
                        c =>
                            c.x ==
                            getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y)
                                .Item1 &&
                            c.y ==
                            getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y)
                                .Item2);

            if (curCell != null)
            {
                if (curCell.x != 3 && curCell.y != 3)
                {
                    curCell.currentState = State.Empty;
                    Rectangle rect = new Rectangle();

                    rect.Width = 30 - 1;
                    rect.Height = 30 - 1;

                    SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    rect.Fill = cellMarker;

                    Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(ruleCanvas).X, e.GetPosition(ruleCanvas).Y,
                        30);
                    Canvas.SetTop(rect, snappedCoords.Item2);
                    Canvas.SetLeft(rect, snappedCoords.Item1);
                    ruleCanvas.Children.Add(rect);
                }
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cell curCell = currNeigh.cells.Find(c => c.x == getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y).Item1 && c.y == getCellCoordsfromCanvas((int)e.GetPosition(ruleCanvas).X, (int)e.GetPosition(ruleCanvas).Y).Item2);

            if (curCell != null)
            {
                if (curCell.currentState == State.Empty || curCell.currentState == State.Dead)
                {
                    curCell.currentState = State.Alive;

                    Rectangle rect = new Rectangle();

                    rect.Width = 30 - 1;
                    rect.Height = 30 - 1;

                    SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    rect.Fill = cellMarker;

                    Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(ruleCanvas).X, e.GetPosition(ruleCanvas).Y,
                        30);
                    Canvas.SetTop(rect, snappedCoords.Item2);
                    Canvas.SetLeft(rect, snappedCoords.Item1);
                    ruleCanvas.Children.Add(rect);
                }
                else if (curCell.currentState == State.Alive)
                {
                    curCell.currentState = State.Dead;

                    Rectangle rect = new Rectangle();

                    rect.Width = 30 - 1;
                    rect.Height = 30 - 1;

                    SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    rect.Fill = cellMarker;

                    Tuple<int, int> snappedCoords = snapToGrid(e.GetPosition(ruleCanvas).X, e.GetPosition(ruleCanvas).Y,
                        30);
                    Canvas.SetTop(rect, snappedCoords.Item2);
                    Canvas.SetLeft(rect, snappedCoords.Item1);
                    ruleCanvas.Children.Add(rect);
                }
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            ruleCanvas.Children.Clear();

            Cell mainCell = currNeigh.cells.Find(c => c.x == 3 && c.y == 3);
            mainCell.currentState = State.Alive;

            Rectangle rect = new Rectangle();

            rect.Width = 30 - 1;
            rect.Height = 30 - 1;

            SolidColorBrush cellMarker = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            rect.Fill = cellMarker;

            Tuple<int, int> snappedCoords = snapToGrid(getCanvasCoordsFromCell(mainCell).Item1 - 1, getCanvasCoordsFromCell(mainCell).Item2 - 1, 30);
            Canvas.SetTop(rect, snappedCoords.Item2);
            Canvas.SetLeft(rect, snappedCoords.Item1);
            ruleCanvas.Children.Add(rect);

            currentRuleText.Text = "Current Rule";

            currCellCount = 0;
            currInitState = State.Empty;
            currFinalState = State.Empty;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {

            if (currentRuleText.Text == "Current Rule")
            {
                currRule = new Rule();
                currRule.Cell = currNeigh.cells.Find(c => c.x == 3 && c.y == 3);
                currRule.initState = currNeigh;
                currRule.finalState = currRule.Cell.currentState;
                currSet.rules.Add(currRule);
                rulesListBox.Items.Add("Rule " + currSet.rules.Count);
                if (currSet.checkRuleSetValidity())
                {
                    
                }
                else
                {
                    if (MessageBox.Show("Rules in set are improper, do you want to fix them manually (yes) or auto (no)?",
                        "Rules not vaild", MessageBoxButton.YesNo,
                        MessageBoxImage.Error, MessageBoxResult.Yes, MessageBoxOptions.None) == MessageBoxResult.Yes)
                    {
                        
                    }
                    else
                    {
                        
                    }
                }
            }
            else
            {
                if (currCellCount != 0)
                {
                    currRule = new Rule();
                    currRule.finalState = currFinalState;
                    currRule.cellCount = currCellCount;
                    currRule.initStateAlternative = currInitState;
                    currSet.rules.Add(currRule);
                    rulesListBox.Items.Add("Rule (text) " + currSet.rules.Count);
                    if (currSet.checkRuleSetValidity())
                    {

                    }
                    else
                    {

                    }
                }
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            currentRuleText.Text = "If " + comboCellCount.Text + " cells in neigh. are " + comboCellState.Text +
                                   " then cell will be " + comboFinalState.Text;
            currCellCount = Convert.ToInt32(comboCellCount.Text);
            if (comboCellState.Text == "Alive")
            {
                currInitState = State.Alive;
            }
            else if (comboCellState.Text == "Dead")
            {
                currInitState = State.Dead;
            }
            else if (comboCellState.Text == "Empty")
            {
                currInitState = State.Empty;
            }

            if (comboFinalState.Text == "Alive")
            {
                currFinalState = State.Alive;
            }
            else if (comboFinalState.Text == "Dead")
            {
                currFinalState = State.Dead;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            currSet.rules.Clear();
            rulesListBox.Items.Clear();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ruleSetList.Add(currSet);
            ruleSetsListBox.Items.Add("Custom Rule Set " + ruleSetList.Count);
            currSet = new RuleSet();
            currSet.rules = new List<Rule>();
        }
    }
}
