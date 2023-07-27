using System.Collections.Generic;
using Source.Data;
using Source.Data.Service;
using UnityEngine;

namespace Source.Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        void InformProgressReaders();
        void RegisterProgressWatchers(GameObject gameObject);
        void Cleanup();
    }
}