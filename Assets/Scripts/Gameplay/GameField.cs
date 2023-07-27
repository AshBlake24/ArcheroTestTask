using UnityEngine;

namespace Source.Gameplay
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private BoxCollider _fieldCollider;

        public Bounds Bounds => _fieldCollider.bounds;
    }
}