using UnityEngine;

namespace Source.Utilities
{
    public static class Helpers
    {
        private static Transform s_generalPoolsContainer;

        public static Transform GetGeneralPoolsContainer()
        {
            if (s_generalPoolsContainer == null)
                s_generalPoolsContainer = new GameObject($"Pools").transform;

            return s_generalPoolsContainer;
        }
    }
}