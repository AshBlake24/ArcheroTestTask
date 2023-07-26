using System;
using System.Collections.Generic;
using System.Linq;
using Source.Infrastructure.Assets;
using UnityEngine;

namespace Source.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<Type, Dictionary<Enum, IStaticData>> _data = 
            new Dictionary<Type, Dictionary<Enum, IStaticData>>();

        public void Load() =>
            LoadAllStaticData();

        public TResult GetDataById<TKey, TResult>(TKey id)
            where TKey : Enum
            where TResult : IStaticData
        {
            KeyValuePair<Type, Dictionary<Enum, IStaticData>> data =
                _data.SingleOrDefault(data => data.Key == typeof(TKey));

            if (data.Equals(default(KeyValuePair<Type, Dictionary<Enum, IStaticData>>)))
                throw new ArgumentNullException($"{typeof(TKey)}", "This data does not exist");

            return (TResult) data.Value.SingleOrDefault(staticData => Equals(staticData.Key, id)).Value;
        }
        
        public IEnumerable<TData> GetAllDataByType<TEnum, TData>() 
            where TEnum : Enum 
            where TData : IStaticData =>
            _data.ContainsKey(typeof(TEnum)) 
                ? _data[typeof(TEnum)].Select(pair => (TData) pair.Value)
                : null;

        private void LoadData()
        {
            Dictionary<Enum, IStaticData> data = Resources
                .LoadAll<ScriptableObject>(AssetPath.StaticDataPath)
                .OfType<IStaticData>()
                .ToDictionary(staticData => staticData.Key);

            foreach (KeyValuePair<Enum, IStaticData> staticData in data)
            {
                Type type = staticData.Key.GetType();
                
                if (_data.ContainsKey(type))
                    _data[type].Add(staticData.Key, staticData.Value);
                else
                    _data[type] = new Dictionary<Enum, IStaticData>() {{staticData.Key, staticData.Value}};
            }
        }

        private void LoadAllStaticData()
        {
            _data.Clear();
            LoadData();
        }
    }
}