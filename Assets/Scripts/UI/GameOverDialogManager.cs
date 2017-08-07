using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDialogManager : MonoBehaviour {
	
	public void OnRestartAccepted() {
		// reload the scene
		SceneManager.LoadScene (1);
	}

	public void OnRestartDeclined() {
		// quit the application
		Application.Quit();
	}
}
