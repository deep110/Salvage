using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour {

    public Text currentScore;
    public Text highScore;

    private void OnEnable() {
        int score = GamePlayManager.Instance.score;
        currentScore.text = score.ToString();

        int hScore = DataManager.Instance.GetAnalyticsData().highScore;
        highScore.text = hScore.ToString();
    }

    public void OnRestartAccepted() {
        // reload the scene
        SceneManager.LoadScene(2);
    }

}
