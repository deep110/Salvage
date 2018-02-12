using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizedTextEditor : EditorWindow {

    public LocalizationData localizationData;

    private string readFilePath;

    [MenuItem("Window/Localized Text Editor")]
    static void Init() {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
    }

    private void OnGUI() {
        if (localizationData != null) {
            var serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save data")) {
                SaveGameData();
            }
        }

        if (GUILayout.Button("Load data")) {
            readFilePath = LoadGameData();
        }

        if (GUILayout.Button("Create new data")) {
            CreateNewData();
        }
    }

    private string LoadGameData() {
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

        if (!string.IsNullOrEmpty(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);

            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }

        return filePath;
    }

    private void SaveGameData() {
        string filePath = (!string.IsNullOrEmpty(readFilePath))
                            ? readFilePath
                            : EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath)) {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataAsJson);
            Debug.Log("File saved successfully");
        }
    }

    private void CreateNewData() {
        localizationData = new LocalizationData();
    }

}