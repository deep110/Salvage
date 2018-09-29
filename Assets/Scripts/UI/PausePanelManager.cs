using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour {

    private AudioManager audioManager;

    private void Start() {
        audioManager = AudioManager.Instance;
    }

    public void OnHomeClicked() {
        audioManager.PlaySound("button_click");
        Time.timeScale = 1;
        UIManager.Instance.setMenuPanelState(false);
        SceneManager.LoadScene(3);
    }

    public void OnPlayClicked() {
        audioManager.PlaySound("button_click");
        Time.timeScale = 1;
        UIManager.Instance.setMenuPanelState(false);
    }

    public void OnSettingsClicked() {
        audioManager.PlaySound("button_click");
        UIManager.Instance.setSettingsPanelState(true);
    }
}
