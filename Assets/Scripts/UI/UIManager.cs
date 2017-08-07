using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject gameOverDialog;

	private GamePlayManager gamePlayManager;
	private Text scoreText;

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
	}

	void Start() {
		gamePlayManager = GamePlayManager.Instance;
		scoreText = transform.GetChild(0).GetComponent<Text>();
	}
	
	void Update() {
		scoreText.text = gamePlayManager.score.ToString();
	}

	void OnDisable() {
		EventManager.GameOverEvent -= gameOver;
	}

	private void gameOver() {
		// show the gameOver dialog
		gameOverDialog.SetActive(true);
	}
}
