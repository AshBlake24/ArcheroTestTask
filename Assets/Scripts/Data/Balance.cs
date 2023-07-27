using System;

namespace Source.Data
{
    [Serializable]
    public class Balance
    {
        public int Coins;
        
        public event Action Changed;

        public void Reset() => Coins = 0;

        public void AddCoins(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));

            Coins += coins;
            Changed?.Invoke();
        }
    }
}