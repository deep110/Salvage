using UnityEngine;

public class TutorialManager : Singleton<TutorialManager> {

	private bool isShowingTutorial;

	void Start () {
		isShowingTutorial = DataManager.Instance.GetSettingsData().isTutorialOn;
	}
}
