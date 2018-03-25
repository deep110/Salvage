using UnityEngine;

public class DataManager : PersistentSingleton<DataManager> {

    public int sessionsCount;
    public int sessionLength;

    private SettingsData settingsData;
    private AnalyticsData analyticsData;

    // settings data keys
    private const string KEY_VFX_ON = "isVfxOn";
    private const string KEY_SOUND_ON = "isSoundOn";
    private const string KEY_TUTORIAL_ON = "isTutorialOn";

    // analytics data keys
    private const string KEY_HIGHSCORE = "highScore";
    private const string KEY_IS_RATED = "isRated";

    private void Start() {
        sessionsCount = 0;
        sessionLength = 0;
    }

    public SettingsData GetSettingsData() {
        if (settingsData == null) {
            settingsData = new SettingsData();

            if (PlayerPrefs.HasKey(KEY_VFX_ON)) {
                settingsData.isVfxOn = PlayerPrefs.GetInt(KEY_VFX_ON) != 0;
            }

            if (PlayerPrefs.HasKey(KEY_SOUND_ON)) {
                settingsData.isSoundOn = PlayerPrefs.GetInt(KEY_SOUND_ON) != 0;
            }

            if (PlayerPrefs.HasKey(KEY_TUTORIAL_ON)) {
                settingsData.isTutorialOn = PlayerPrefs.GetInt(KEY_TUTORIAL_ON) != 0;
            }
        }

        return settingsData;
    }

    public void SetSettingsData(SettingsData settingsData) {
        PlayerPrefs.SetInt(KEY_VFX_ON, settingsData.isVfxOn ? 1 : 0);
        PlayerPrefs.SetInt(KEY_SOUND_ON, settingsData.isSoundOn ? 1 : 0);
        PlayerPrefs.SetInt(KEY_TUTORIAL_ON, settingsData.isTutorialOn ? 1 : 0);

        this.settingsData = settingsData;
    }

    public void SetAnalyticsData(AnalyticsData analyticsData) {
        PlayerPrefs.SetInt(KEY_HIGHSCORE, analyticsData.highScore);
        PlayerPrefs.SetInt(KEY_IS_RATED, (int)analyticsData.isRated);
    }

    public AnalyticsData GetAnalyticsData() {
        if (analyticsData == null) {
            analyticsData = new AnalyticsData();

            if (PlayerPrefs.HasKey(KEY_HIGHSCORE)) {
                analyticsData.highScore = PlayerPrefs.GetInt(KEY_HIGHSCORE);
            }

            if (PlayerPrefs.HasKey(KEY_IS_RATED)) {
                analyticsData.isRated = (AnalyticsData.RateState)PlayerPrefs.GetInt(KEY_IS_RATED);
            }
        }

        return analyticsData;
    }
}