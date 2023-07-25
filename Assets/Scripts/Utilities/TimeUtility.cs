using System.Collections.Generic;
using UnityEngine;

namespace Source.Utilities
{
    public static class TimeUtility
    {
        private static readonly Dictionary<float, WaitForSeconds> s_waitDictionary = new Dictionary<float, WaitForSeconds>();
        
        public static WaitForSeconds GetTime(float timeInSeconds)
        {
            if (s_waitDictionary.TryGetValue(timeInSeconds, out WaitForSeconds wait))
                return wait;

            s_waitDictionary[timeInSeconds] = new WaitForSeconds(timeInSeconds);

            return s_waitDictionary[timeInSeconds];
        }
    }
}