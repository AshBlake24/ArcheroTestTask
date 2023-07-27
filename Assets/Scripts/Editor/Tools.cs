using System.IO;
using Source.Data.Service;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.SaveLoadService;
using Source.Utilities;
using UnityEditor;
using UnityEngine;

namespace Source.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        
        [MenuItem("Tools/Save Progress To JSON")]
        public static void SaveProgress()
        {
            if (EditorApplication.isPlaying == false)
                return;
            
            ServiceLocator.Container
                .Single<ISaveLoadService>()
                .SaveProgress();

            string dataToStore = ServiceLocator.Container
                .Single<IPersistentDataService>()
                .PlayerProgress
                .ToJson(prettyPrint: true);
            
            SaveToFile(dataToStore);
            
            Debug.Log("Progress Saved");
        }
        
        private static void SaveToFile(string dataToStore)
        {
            string path = Path.Combine(Application.persistentDataPath, "Data.json");

            using FileStream fileStream = new FileStream(path, FileMode.Create);
            using StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.Write(dataToStore);
        }
    }
}