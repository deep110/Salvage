using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    public Text scorePanel;

    public GameObject startText;
    public GameObject menuPanel;
    public GameObject platformClearPanel;
    public GameObject settingsPanel;
    public GameObject gameOverPanel;
    public GameObject playerRevivePanel;

    public void OnPauseButtonClick() {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
    }

    public void UpdateScoreText(int score) {
        scorePanel.text = score.ToString();
    }

    public void ShowPlatformClear() {
        platformClearPanel.SetActive(true);
        Invoke("HidePlatformClear", 2f);
    }

    private void HidePlatformClear() {
        platformClearPanel.SetActive(false);
    }

    public void setSettingsPanelState(bool open) {
        settingsPanel.SetActive(open);
    }

    public void setGameOverPanelState(bool open) {
        gameOverPanel.SetActive(open);
    }

    public void setPlayerRevivePanelState(bool open) {
        playerRevivePanel.SetActive(open);
    }

    public void setStartTextState(bool shown) {
        startText.SetActive(shown);
    }
}
