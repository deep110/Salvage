using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour {

    public void OnHomeClicked() {
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    public void OnPlayClicked() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnSettingsClicked() {
        UIManager.Instance.setSettingsPanelState(true);
    }
}
