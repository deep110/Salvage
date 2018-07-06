using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour {

    public GameObject settingsPanel;

    public void LoadGame() {
        SettingsData settingsData = DataManager.Instance.GetSettingsData();
        if (settingsData.isTutorialOn) {
            // update Settings Data
            settingsData.isTutorialOn = false;
            DataManager.Instance.SetSettingsData(settingsData);

            // load tutorial scene
            SceneManager.LoadScene(3);
        } else {
            // load game
            SceneManager.LoadScene(2);
        }
    }

    public void OpenSettings() {
        settingsPanel.SetActive(true);
    }
}
