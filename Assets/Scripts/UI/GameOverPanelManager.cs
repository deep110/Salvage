using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour {

    public void OnRestartAccepted() {
        // reload the scene
        SceneManager.LoadScene(2);
    }

    public void OnRestartDeclined() {
        // quit the application
        Application.Quit();
    }
}
