using System.Collections.Generic;
using Source.Data;
using Source.Data.Service;
using Source.Utilities;
using UnityEngine;

namespace Source.Infrastructure.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerProgressKey = "PlayerProgress";

        private readonly IPersistentDataService _persistentDataService;
        private readonly List<IProgressReader> _progressReaders;
        private readonly List<IProgressWriter> _progressWriters;

        public SaveLoadService(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
            _progressReaders = new List<IProgressReader>();
            _progressWriters = new List<IProgressWriter>();
        }

        public void SaveProgress()
        {
            foreach (IProgressWriter progressWriter in _progressWriters)
                progressWriter.WriteProgress(_persistentDataService.PlayerProgress);

            string dataToStore = _persistentDataService.PlayerProgress.ToJson(prettyPrint: false);
            PlayerPrefs.SetString(PlayerProgressKey, dataToStore);
            PlayerPrefs.Save();
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(PlayerProgressKey)?
                .FromJson<PlayerProgress>();

        public void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _progressReaders)
                progressReader.ReadProgress(_persistentDataService.PlayerProgress);
        }

        public void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (IProgressReader progressReader in gameObject.GetComponentsInChildren<IProgressReader>())
                Register(progressReader);
        }

        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        private void Register(IProgressReader progressReader)
        {
            if (progressReader is IProgressWriter progressWriter)
                _progressWriters.Add(progressWriter);

            _progressReaders.Add(progressReader);
        }
    }
}