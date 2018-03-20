using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour {

    public Text currentScore;
    public Text highScore;

    private void OnEnable() {
        int score = GamePlayManager.Instance.score;
        currentScore.text = score.ToString();

        PlayerData playerData = DataManager.Instance.GetData();

        if (playerData.highScore < score) {
            highScore.text = score.ToString();

            // update high score to prefs
            playerData.highScore = score;
            DataManager.Instance.SetData(playerData);
        } else {
            highScore.text = playerData.highScore.ToString();
        }
    }

    public void OnRestartAccepted() {
        // reload the scene
        SceneManager.LoadScene(2);
    }

    
}
