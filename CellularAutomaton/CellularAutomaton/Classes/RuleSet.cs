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
            Cell updatedCell = init.cells.Find(c => c.x == 3 && c.y == 3);
            foreach (Rule rule in rules)
            {
                if (rule.cellCount == 0)
                {
                    bool same = false;
                    for (int i = 0; i < rule.initState.cells.Count; i++)
                    {
                        Cell currCell = rule.initState.cells[i];
                        Cell vsCell = init.cells[i];

                        if ((currCell.x == 3 && currCell.y == 3) || (vsCell.x == 3 && vsCell.y == 3))
                        {
                            continue;
                        }

                        if (currCell.currentState == vsCell.currentState)
                        {
                            same = true;
                        }
                        else
                        {
                            same = false;
                            break;
                        }
                    }

                    if (same)
                    {
                        updatedCell.currentState = rule.finalState;
                    }
                }
                else
                {
                    int count = 0;
                    for (int i = 0; i < rule.initState.cells.Count; i++)
                    {
                        Cell vsCell = init.cells[i];

                        if (vsCell.x == 3 && vsCell.y == 3)
                        {
                            continue;
                        }

                        if (rule.initStateAlternative == vsCell.currentState)
                        {
                            count++;
                        }
                    }

                    if (count == rule.cellCount)
                    {
                        updatedCell.currentState = rule.finalState;
                    }
                }
            }

            return updatedCell;
        }

        public int checkRuleSetValidity()
        {
            bool proper = true;
            int wrongRuleMarker = -1;

            for (int i = 0; i < rules.Count; i++)
            {
                Rule itRule = rules[i];
                if (itRule.cellCount != 0)
                {
                    for (int j = 0; j < rules.Count; j++)
                    {
                        Rule vsRule = rules[j];
                        if (vsRule.cellCount != 0)
                        {
                            if ((itRule.cellCount == vsRule.cellCount) && (itRule.finalState != vsRule.finalState) &&
                                (itRule.initStateAlternative == vsRule.initStateAlternative))
                            {
                                proper = false;
                                wrongRuleMarker = j;
                                break;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (proper == false)
                        break;
                }
                else
                {
                    for (int j = 0; j < rules.Count; j++)
                    {
                        Rule vsRule = rules[j];
                        bool same = false;
                        if (itRule.finalState != vsRule.finalState)
                        {
                            for (int x = 0; x < itRule.initState.cells.Count; x++)
                            {
                                Cell currCell = itRule.initState.cells[x];
                                Cell vsCell = vsRule.initState.cells[x];

                                if ((currCell.x == 3 && currCell.y == 3) || (vsCell.x == 3 && vsCell.y == 3))
                                {
                                    continue;
                                }

                                if (currCell.currentState == vsCell.currentState)
                                {
                                    same = true;
                                }
                                else
                                {
                                    same = false;
                                    break;
                                }
                            }
                        }

                        if (same == true)
                        {
                            proper = false;
                            wrongRuleMarker = j;
                            break;
                        }
                    }

                    if (proper == false)
                        break;
                }
            }

            return wrongRuleMarker;
        }

    }
}

