using System.Collections.Generic;

namespace CellularAutomaton
{
    public class RuleSet
    {
        public List<Rule> rules
        {
            get;
            set;
        }

        public Simulation Simulation
        {
            get;
            set;
        }

        public Rule getRule(int i)
        {
            if (i >= 0 && i <= rules.Count)
            {
                return rules[i];
            }
            else
            {
                return null;
            }
        }

        public Cell applyRules(Neighborhood init)
        {
            return new Cell();
        }

        public bool checkRuleSetValidity()
        {
            bool proper = true;
            int wrongRuleMarker = -1;

            foreach (Rule r in rules)
            {
                State lastState = r.finalState;
                bool same = false;
                for (int i = 0; i < r.initState.cells.Count; i++)
                {
                    Cell currCell = r.initState.cells[i];
                    if(currCell.x == 3 && currCell.y == 3) { continue; }

                    //if(currCell.)
                }
            }
            return true;
        }

    }
}

