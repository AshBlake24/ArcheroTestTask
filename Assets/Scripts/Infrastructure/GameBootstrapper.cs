using UnityEngine;

namespace Source.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapper;

        private void Awake()
        {
            Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();

            if (bootstrapper == null)
                Instantiate(_bootstrapper);
        }
    }
}