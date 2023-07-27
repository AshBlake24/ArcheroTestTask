using System;

namespace Source.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State State;
        public Balance Balance;
        
        public PlayerProgress()
        {
            State = new State();
            Balance = new Balance();
        }
    }
}