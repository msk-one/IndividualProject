using System.Collections.Generic;

namespace CellularAutomaton
{
    public class Grid
    {
        public virtual List<List<Cell>> cells
        {
            get;
            set;
        }

        public virtual Simulation Simulation
        {
            get;
            set;
        }

        public virtual void clear()
        {
            throw new System.NotImplementedException();
        }

        public virtual void generate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void zoomin()
        {
            throw new System.NotImplementedException();
        }

        public virtual void zoomout()
        {
            throw new System.NotImplementedException();
        }

    }
}

