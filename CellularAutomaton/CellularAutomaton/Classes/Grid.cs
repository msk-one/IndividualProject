using System.Collections.Generic;

namespace CellularAutomaton
{
    public class Grid
    {
        public List<Cell> cells
        {
            get;
            set;
        }

        public Simulation Simulation
        {
            get;
            set;
        }

        public void clear()
        {
            cells.Clear();
        }

    }
}

