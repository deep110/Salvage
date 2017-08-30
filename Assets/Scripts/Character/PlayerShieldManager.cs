using UnityEngine;

public class PlayerShieldManager : MonoBehaviour {

	public GameObject shield;

	private Renderer _renderer;

	void Awake() {
		_renderer = GetComponent<Renderer>();
	}

	private void ActivateShield() {
		shield.SetActive(true);
	}

}