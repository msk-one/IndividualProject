using System.Collections.Generic;
using System.Windows.Controls;

namespace CellularAutomaton
{
    public class Simulation
    {
        public Grid grid
        {
            get;
            set;
        }

        private int tick
        {
            get;
            set;
        }

        private RuleSet ruleSet
        {
            get;
            set;
        }

        public Canvas gridCanvas
        {
            get;
            set;
        }

        public List<Grid> backlogGrids
        {
            get;
            set;
        }

        public void play()
        {
            throw new System.NotImplementedException();
        }

        public void pause()
        {
            throw new System.NotImplementedException();
        }

        public void stop()
        {
            throw new System.NotImplementedException();
        }

        public void changeRuleSet(RuleSet rs)
        {
            throw new System.NotImplementedException();
        }

        public void clearRuleSet()
        {
            throw new System.NotImplementedException();
        }

        public void getGridFromHistory(int i)
        {
            throw new System.NotImplementedException();
        }

    }
}

