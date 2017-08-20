using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject menuPanel;

	public void OnPauseButtonClick() {
		Time.timeScale = 0;
		menuPanel.SetActive(true);
	}
}
