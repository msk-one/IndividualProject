using System.Collections.Generic;

namespace CellularAutomaton
{
    public class Neighborhood
    {
        private int size
        {
            get;
            set;
        }

        public virtual List<List<Cell>> cells
        {
            get;
            set;
        }

        public virtual int getSize()
        {
            throw new System.NotImplementedException();
        }

        public virtual void setSize(int sz)
        {
            throw new System.NotImplementedException();
        }

    }
}

