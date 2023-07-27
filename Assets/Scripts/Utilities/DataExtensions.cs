using UnityEngine;

namespace Source.Utilities
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj, bool prettyPrint) => 
            JsonUtility.ToJson(obj, prettyPrint);

        public static T FromJson<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}