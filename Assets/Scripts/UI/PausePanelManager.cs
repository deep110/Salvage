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

	///<TODO>
	/// make a settings dialog and set Time Scale 1
	/// when dialog is closed.
	///</TODO>
	public void OnSettingsClicked () {
		gameObject.SetActive(false);
		Time.timeScale = 1;
	}
}
