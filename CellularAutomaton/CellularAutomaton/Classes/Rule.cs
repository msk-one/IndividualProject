using System.Collections.Generic;

namespace CellularAutomaton
{
    public class Rule
    {
        public Neighborhood initState
        {
            get;
            set;
        }

        public State finalState
        {
            get;
            set;
        }

        public State initStateAlternative
        {
            get;
            set;
        }

        public int cellCount
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

    }
}

