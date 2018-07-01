using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelManager : MonoBehaviour {

	public Toggle sound;
	public Toggle vfx;
	public Toggle tutorial;

	private SettingsData settingsData;

	private void OnEnable() {
		settingsData = DataManager.Instance.GetSettingsData();

		sound.isOn = settingsData.isSoundOn;
		vfx.isOn = settingsData.isVfxOn;
		tutorial.isOn = settingsData.isTutorialOn;
	}

	public void ToogleSound(bool isSelected) {
		Debug.Log("Sound: " + isSelected);
	}

	public void ToogleVfx(bool isSelected) {
		Debug.Log("Vfx: "+ isSelected);
	}

	public void ToogleTutorial(bool isSelected) {
		//
	}

	public void closePanel() {
		gameObject.SetActive(false);

		settingsData.isSoundOn = sound.isOn;
		settingsData.isVfxOn = vfx.isOn;
		settingsData.isTutorialOn = tutorial.isOn;

		DataManager.Instance.SetSettingsData(settingsData);
	}
}

