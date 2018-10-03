using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager> {

    public TMP_Text scoreText;

    [Header("Buttons")]
    public GameObject pauseButton;
    public GameObject homeButton;

    [Header("Panels")]
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

    public void OnHomeButtonClick() {
        Time.timeScale = 1;
        AudioManager.Instance.PlaySound("button_click");
        SceneManager.LoadScene(1);
    }

    public void UpdateScoreText(int score) {
        scoreText.text = score.ToString();
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

    public void setPauseButtonState(bool shown) {
        if (shown) {
            pauseButton.SetActive(true);
            homeButton.SetActive(false);
        } else {
            pauseButton.SetActive(false);
            homeButton.SetActive(true);
        }
    }
}
