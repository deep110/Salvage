using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour {

    public void OnHomeClicked() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public void OnPlayClicked() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnSettingsClicked() {
        UIManager.Instance.setSettingsPanelState(true);
    }
}
