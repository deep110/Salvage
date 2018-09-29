using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LocalizationItem {
    public string key;
    public string value;
}

[System.Serializable]
public class LocalizationData {
    public LocalizationItem[] items;
}


public class LocalizationManager : PersistentSingleton<LocalizationManager> {

    private Dictionary<string, string> localizedText;
    private bool isReady;

    public void LoadLocalizedText(string languageName) {

        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, "text_" + languageName + ".json");

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++) {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
        } else {
            Debug.LogError("Cannot find file!");
            isReady = false;
        }

        isReady = true;
    }

    public string GetLocalizedValue(string key) {
        return localizedText[key];
    }

    public bool IsReady() {
        return Instance != null && isReady;
    }

}
