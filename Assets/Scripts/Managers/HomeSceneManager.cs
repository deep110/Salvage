using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour {

    public GameObject settingsPanel;

    public void LoadScene(int level) {
        SceneManager.LoadScene(level);
    }

    public void OpenSettings() {
        settingsPanel.SetActive(true);
    }
}
