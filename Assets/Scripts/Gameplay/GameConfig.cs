using UnityEngine;

namespace Source.Gameplay
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Static Data/Game Config")]
    public class GameConfig : ScriptableObject
    {
        public const string LevelName = "Game";
        
        [SerializeField] private int _secondsBeforeGameStart;
        [SerializeField] private int _timeToRestart;

        public int SecondsBeforeGameStart => _secondsBeforeGameStart;
        public float TimeToRestart => _timeToRestart;
    }
}