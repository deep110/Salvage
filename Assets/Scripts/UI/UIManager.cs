using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GamePlayManager gamePlayManager;
	private Text scoreText;

	void Start () {
		gamePlayManager = GamePlayManager.Instance;
		scoreText = transform.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = gamePlayManager.score.ToString();
	}
}
