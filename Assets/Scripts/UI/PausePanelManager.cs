using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour {

    public void OnHomeClicked() {
        AudioManager.Instance.PlaySound("button_click");
        Time.timeScale = 1;
        UIManager.Instance.setMenuPanelState(false);
        SceneManager.LoadScene(3);
    }

    public void OnPlayClicked() {
        AudioManager.Instance.PlaySound("button_click");
        Time.timeScale = 1;
        UIManager.Instance.setMenuPanelState(false);
    }

    public void OnSettingsClicked() {
        AudioManager.Instance.PlaySound("button_click");
        UIManager.Instance.setSettingsPanelState(true);
    }
}
