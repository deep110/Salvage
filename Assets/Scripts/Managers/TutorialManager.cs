using System.Collections;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager> {

	private SettingsData settingsData;

	private IEnumerator Start() {
		settingsData = DataManager.Instance.GetSettingsData();
		if (settingsData.isTutorialOn) {
			// show tutorial
			

			// update Settings Data
			// settingsData.isTutorialOn = false;
			// DataManager.Instance.SetSettingsData(settingsData);
		} else {
			GamePlayManager.Instance.StartGame();
		}
		
		// now start the game
		// GamePlayManager.Instance.StartGame();
		yield return null;
	}
}
