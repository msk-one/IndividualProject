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
            foreach (Rule r in rules)
            {
                
            }
            return true;
        }

    }
}

