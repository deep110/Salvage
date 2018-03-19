using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelManager : MonoBehaviour {

	public Toggle sound;
	public Toggle vfx;
	public Toggle tutorial;

	private PlayerData playerData;

	private void Awake() {
		playerData = DataManager.Instance.GetData();

		sound.isOn = playerData.isSoundOn;
		vfx.isOn = playerData.isVfxOn;
		tutorial.isOn = playerData.isTutorialOn;
	}

	public void ToogleSound(bool isSelected) {
		Debug.Log("Sound: " + isSelected);
	}

	public void ToogleVfx(bool isSelected) {
		Debug.Log("Vfx: "+ isSelected);
	}

	public void ToogleTutorial(bool isSelected) {
		Debug.Log("Tutorial: "+ isSelected);
	}

	public void closePanel() {
		UIManager.Instance.setSettingsPanelState(false);

		playerData.isSoundOn = sound.isOn;
		playerData.isVfxOn = vfx.isOn;
		playerData.isTutorialOn = tutorial.isOn;

		DataManager.Instance.SetData(playerData);
	}
}

