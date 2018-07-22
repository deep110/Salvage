using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverPanelManager : MonoBehaviour {

    public TMP_Text currentScore;
    public TMP_Text highScore;

    private void OnEnable() {
        int score = GamePlayManager.Instance.score;
        currentScore.text = score.ToString() + "\n<size=40%>Your Score";

        int hScore = DataManager.Instance.GetAnalyticsData().highScore;
        highScore.text = hScore.ToString() + "\n<size=50%>High Score";
    }

    public void OnRestartAccepted() {
        // reload the scene
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

}
