using System.Collections.Generic;

namespace CellularAutomaton
{
    public class Rule
    {
        private Neighborhood initState
        {
            get;
            set;
        }

        private State finalState
        {
            get;
            set;
        }

        private Neighborhood currNeighborhood
        {
            get;
            set;
        }

        private Cell currCell
        {
            get;
            set;
        }

        public virtual RuleSet RuleSet
        {
            get;
            set;
        }

        public virtual Cell Cell
        {
            get;
            set;
        }

        public virtual IEnumerable<Neighborhood> Neighborhood
        {
            get;
            set;
        }

        public virtual State State
        {
            get;
            set;
        }

        public virtual Cell getCurrCell()
        {
            throw new System.NotImplementedException();
        }

        public virtual Neighborhood getNeighborhood()
        {
            throw new System.NotImplementedException();
        }

        public virtual State getFinalState()
        {
            throw new System.NotImplementedException();
        }

        public virtual Neighborhood getInitState()
        {
            throw new System.NotImplementedException();
        }

        public virtual void setCurrCell(Cell c)
        {
            throw new System.NotImplementedException();
        }

        public virtual void setFinalState(State s1)
        {
            throw new System.NotImplementedException();
        }

        public virtual void setInitState(Neighborhood s0)
        {
            throw new System.NotImplementedException();
        }

        public virtual void setNeighborhood(Neighborhood n)
        {
            throw new System.NotImplementedException();
        }

    }
}

