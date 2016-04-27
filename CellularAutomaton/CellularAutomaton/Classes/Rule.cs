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

        public RuleSet RuleSet
        {
            get;
            set;
        }

        public Cell Cell
        {
            get;
            set;
        }

        public List<Neighborhood> Neighborhood
        {
            get;
            set;
        }

        public State State
        {
            get;
            set;
        }

    }
}

