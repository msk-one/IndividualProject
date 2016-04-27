namespace CellularAutomaton
{
    public class State
    {
        private string name
        {
            get;
            set;
        }

        private int type
        {
            get;
            set;
        }

        public virtual Cell Cell
        {
            get;
            set;
        }

        public virtual string getName()
        {
            throw new System.NotImplementedException();
        }

        public virtual void setName(string n)
        {
            throw new System.NotImplementedException();
        }

        public virtual int getType()
        {
            throw new System.NotImplementedException();
        }

        public virtual void setType(int t)
        {
            throw new System.NotImplementedException();
        }

    }
}

