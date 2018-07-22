using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour {

    public void OnHomeClicked() {
        Time.timeScale = 1;
        UIManager.Instance.setMenuPanelState(false);
        SceneManager.LoadScene(3);
    }

    public void OnPlayClicked() {
        Time.timeScale = 1;
        UIManager.Instance.setMenuPanelState(false);
    }

    public void OnSettingsClicked() {
        UIManager.Instance.setSettingsPanelState(true);
    }
}
