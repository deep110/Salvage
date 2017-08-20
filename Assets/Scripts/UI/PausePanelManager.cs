using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour {

	public void OnHomeClicked() {
		SceneManager.LoadScene (0);
		gameObject.SetActive(false);
	}

	public void OnPlayClicked () {
		Time.timeScale = 1;
		gameObject.SetActive(false);
	}

	public void OnSettingsClicked () {
		// coming soon
		gameObject.SetActive(false);
		Time.timeScale = 1;
	}
}
