using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager> {

    public TMP_Text scorePanel;

    public GameObject startText;
    public GameObject menuPanel;
    public GameObject platformClearPanel;
    public GameObject settingsPanel;
    public GameObject gameOverPanel;
    public GameObject playerRevivePanel;
    public GameObject backGroundImage;

    public void OnPauseButtonClick() {
        Time.timeScale = 0;
        AudioManager.Instance.PlaySound("button_click");
        setMenuPanelState(true);
    }

    public void UpdateScoreText(int score) {
        scorePanel.text = score.ToString();
    }

    public void ShowPlatformClear() {
        platformClearPanel.SetActive(true);
        Invoke("HidePlatformClear", 1.4f);
    }

    private void HidePlatformClear() {
        platformClearPanel.SetActive(false);
    }

    public void setMenuPanelState(bool open) {
        menuPanel.SetActive(open);
        backGroundImage.SetActive(open);
    }

    public void setSettingsPanelState(bool open) {
        settingsPanel.SetActive(open);
        backGroundImage.SetActive(open);
    }

    public void setGameOverPanelState(bool open) {
        gameOverPanel.SetActive(open);
        backGroundImage.SetActive(open);
    }

    public void setPlayerRevivePanelState(bool open) {
        playerRevivePanel.SetActive(open);
        backGroundImage.SetActive(open);
    }

    public void setStartTextState(bool shown) {
        startText.SetActive(shown);
    }
}
