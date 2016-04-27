namespace CellularAutomaton
{
    public class Cell
    {
        private int x
        {
            get;
            set;
        }

        private int y
        {
            get;
            set;
        }

        private State currentState
        {
            get;
            set;
        }

        public Grid Grid
        {
            get;
            set;
        }

        public Neighborhood Neighborhood
        {
            get;
            set;
        }

    }
}

