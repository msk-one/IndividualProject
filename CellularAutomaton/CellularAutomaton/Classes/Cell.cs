namespace CellularAutomaton
{
    public class Cell
    {
        public int x
        {
            get;
            set;
        }

        public int y
        {
            get;
            set;
        }

        public State currentState
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

