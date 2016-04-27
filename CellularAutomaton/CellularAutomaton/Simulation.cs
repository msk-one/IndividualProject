using System.Collections.Generic;
using System.Windows.Controls;

namespace CellularAutomaton
{
    public class Simulation
    {
        private Grid grid
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

        public virtual Canvas gridCanvas
        {
            get;
            set;
        }

        public virtual List<Grid> backlogGrids
        {
            get;
            set;
        }

        public virtual void play()
        {
            throw new System.NotImplementedException();
        }

        public virtual void pause()
        {
            throw new System.NotImplementedException();
        }

        public virtual void stop()
        {
            throw new System.NotImplementedException();
        }

        public virtual void changeRuleSet(RuleSet rs)
        {
            throw new System.NotImplementedException();
        }

        public virtual void clearRuleSet()
        {
            throw new System.NotImplementedException();
        }

        public virtual Grid getGrid()
        {
            throw new System.NotImplementedException();
        }

        public virtual RuleSet getRuleSet()
        {
            throw new System.NotImplementedException();
        }

        public virtual int getTick()
        {
            throw new System.NotImplementedException();
        }

        public virtual void setRuleSet(RuleSet rs)
        {
            throw new System.NotImplementedException();
        }

        public virtual void getGridFromHistory(int i)
        {
            throw new System.NotImplementedException();
        }

    }
}

