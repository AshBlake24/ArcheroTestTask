using UnityEngine;

namespace Source.Gameplay
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Static Data/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _secondsBeforeGameStart;

        public int SecondsBeforeGameStart => _secondsBeforeGameStart;
    }
}