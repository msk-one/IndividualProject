using System.Collections.Generic;

namespace CellularAutomaton
{
    public class RuleSet
    {
        private List<Rule> rules
        {
            get;
            set;
        }

        public virtual Simulation Simulation
        {
            get;
            set;
        }

        public virtual void getRule(int i)
        {
            throw new System.NotImplementedException();
        }

        public virtual void applyRules()
        {
            throw new System.NotImplementedException();
        }

        public virtual void checkRuleSetValidity()
        {
            throw new System.NotImplementedException();
        }

    }
}

