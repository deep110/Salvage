using UnityEngine;

public class DataManager : PersistentSingleton<DataManager> {

    private PlayerData playerData;

    protected override void Awake() {
        base.Awake();
        playerData = new PlayerData();
    }

    public PlayerData GetData() {
        
        if (PlayerPrefs.HasKey("highScore")) {
            playerData.highScore = PlayerPrefs.GetInt("highScore");
        }

        if (PlayerPrefs.HasKey("isVfxOn")) {
            playerData.isVfxOn = PlayerPrefs.GetInt("isVfxOn") != 0;
        }

        if (PlayerPrefs.HasKey("isSoundOn")) {
            playerData.isSoundOn = PlayerPrefs.GetInt("isSoundOn") != 0;
        }

        if (PlayerPrefs.HasKey("isTutorialOn")) {
            playerData.isTutorialOn = PlayerPrefs.GetInt("isTutorialOn") != 0;
        }

        return playerData;
    }

    public void SetData(PlayerData playerData) {
        PlayerPrefs.SetInt("highScore", playerData.highScore);
        PlayerPrefs.SetInt("isVfxOn", playerData.isVfxOn ? 1 : 0);
        PlayerPrefs.SetInt("isSoundOn", playerData.isSoundOn ? 1 : 0);
        PlayerPrefs.SetInt("isTutorialOn", playerData.isTutorialOn ? 1 : 0);
    }
}