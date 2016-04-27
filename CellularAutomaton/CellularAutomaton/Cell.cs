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

        public virtual Grid Grid
        {
            get;
            set;
        }

        public virtual Neighborhood Neighborhood
        {
            get;
            set;
        }

        public virtual int getX()
        {
            throw new System.NotImplementedException();
        }

        public virtual int getY()
        {
            throw new System.NotImplementedException();
        }

        public virtual State getState()
        {
            throw new System.NotImplementedException();
        }

        public virtual void setState(State s)
        {
            throw new System.NotImplementedException();
        }

        public virtual void setX(int x)
        {
            throw new System.NotImplementedException();
        }

        public virtual void setY(int y)
        {
            throw new System.NotImplementedException();
        }

    }
}

