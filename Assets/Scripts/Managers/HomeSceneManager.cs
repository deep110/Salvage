using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeSceneManager : MonoBehaviour {

    public GameObject settingsPanel;
    public TMP_Text highScore;
    public string playStoreId;

    private DataManager dataManager;

    private void Start() {
        dataManager = DataManager.Instance;
        highScore.text = dataManager.GetAnalyticsData().highScore.ToString();

        // play background music
        AudioManager.Instance.PlaySound("background_music");
    }

    public void LoadGame() {
        AudioManager.Instance.PlaySound("button_click");

        SettingsData settingsData = DataManager.Instance.GetSettingsData();
        if (settingsData.isTutorialOn) {
            // update Settings Data
            settingsData.isTutorialOn = false;
            DataManager.Instance.SetSettingsData(settingsData);

            // load tutorial scene
            SceneManager.LoadScene(2);
        } else {
            // load game
            SceneManager.LoadScene(3);
        }
    }

    public void OpenSettings() {
        AudioManager.Instance.PlaySound("button_click");
        settingsPanel.SetActive(true);
    }

    public void RateUs() {
        AudioManager.Instance.PlaySound("button_click");
        // open playstore
        Application.OpenURL("market://details?id=" + playStoreId);

        // update rate state
        AnalyticsData analyticsData = dataManager.GetAnalyticsData();
        analyticsData.isRated = AnalyticsData.RateState.RATED;
        dataManager.SetAnalyticsData(analyticsData);
    }
}
