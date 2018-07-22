using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelManager : MonoBehaviour {

	public Toggle music;
	public Toggle sfx;
	public Toggle tutorial;

	private SettingsData settingsData;

	private void OnEnable() {
		settingsData = DataManager.Instance.GetSettingsData();

		music.isOn = settingsData.isSoundOn;
		sfx.isOn = settingsData.isVfxOn;
		tutorial.isOn = settingsData.isTutorialOn;
	}

	public void ToogleSound(bool isSelected) {
		if (music.isOn) {
			AudioManager.Instance.PlaySound("background_music", true);
		} else {
			AudioManager.Instance.StopSound("background_music");
		}
	}

	public void ToogleVfx(bool isSelected) {}

	public void ToogleTutorial(bool isSelected) {
		//
	}

	public void closePanel() {
		AudioManager.Instance.PlaySound("button_click");
		gameObject.SetActive(false);

		settingsData.isSoundOn = music.isOn;
		settingsData.isVfxOn = sfx.isOn;
		settingsData.isTutorialOn = tutorial.isOn;

		DataManager.Instance.SetSettingsData(settingsData);
	}
}

