using System.Collections;
using UnityEngine;

/**
* Act as top level manager for the game
* handles the game between start and till
* game is over.
*/
public class GamePlayManager : Singleton<GamePlayManager> {

    public enum GameState {
        TO_BE_STARTED,
        RUNNING,
        PAUSED,
        ENDED,
    }

    public int score;
    public int platformsClimbed;
    public int revialChancesLeft = 1;

    private GameState gameState;
    private UIManager uiManager;
    private DataManager dataManager;
    private float sessionStartTime = 0;

    private void OnEnable() {
        Time.timeScale = 1;
        gameState = GameState.TO_BE_STARTED;
        uiManager = UIManager.Instance;
        dataManager = DataManager.Instance;
        sessionStartTime = Time.realtimeSinceStartup;
    }

    private IEnumerator Start() {
        AudioManager.Instance.PlaySound("background_music");

        EventManager.CrystalCollectEvent += onCrystalCollected;
        EventManager.PlatformClimbEvent += onPlatformClimbed;
        EventManager.PlatformClearEvent += onPlatformClear;

        uiManager.setStartTextState(true);
        yield return new WaitForSeconds(2f);

        // start the game
        uiManager.setStartTextState(false);
        gameState = GameState.RUNNING;
    }

    public void UpdateRevival(bool revivalAccepted) {
        // disable the revival dialog
        uiManager.setPlayerRevivePanelState(false);

        if (revivalAccepted) {
            Time.timeScale = 1;
            gameState = GameState.RUNNING;

            // revive players
            PlayerManager.Instance.RevivePlayers();

            // revival accepted, play music again
            AudioManager.Instance.PlaySound("background_music");
        } else {
            // end the game
            makeGameEnd();
        }
    }

    public void OnGameOver() {
        // pause the game
        Time.timeScale = 0;
        // update analytics to local storage
        updateAnalytics();
        // stop background music
        AudioManager.Instance.StopSound("background_music");

        if (revialChancesLeft > 0 && AdsManager.Instance.IsReady(true)) {
            revialChancesLeft = revialChancesLeft - 1;
            gameState = GameState.ENDED;
            uiManager.setPlayerRevivePanelState(true);
        } else {
            // end the game
            makeGameEnd();
        }
    }

    public GameState getGameState() {
        return gameState;
    }

    //--------------------------------------------------------------------------------
    // PRIVATE METHODS
    //--------------------------------------------------------------------------------

    private void makeGameEnd() {
        gameState = GameState.ENDED;

        // update session count
        dataManager.sessionsCount += 1;
        bool isShown = RateBox.Instance.Show();

        // don't show ads when ratebox is shown
        if (!isShown) {
            // show ad
            if (dataManager.sessionsCount % 4 == 0 && AdsManager.Instance.IsReady(false)) {
                AdsManager.Instance.ShowSimpleAd();
            }
        }
        // show gameOver Dialog
        uiManager.setGameOverPanelState(true);

        // unsubscribe events
        EventManager.CrystalCollectEvent -= onCrystalCollected;
        EventManager.PlatformClimbEvent -= onPlatformClimbed;
        EventManager.PlatformClearEvent -= onPlatformClear;
    }

    private void onCrystalCollected() {
        score++;
        uiManager.UpdateScoreText(score);
    }

    private void onPlatformClimbed(int platforms) {
        platformsClimbed = platforms;
    }

    private void onPlatformClear() {
        //increase the score by 2
        score += 2;
        //display the platform clear panel
        uiManager.ShowPlatformClear();
        //update the ui score
        uiManager.UpdateScoreText(score);
    }

    private void updateAnalytics() {
        AnalyticsData analyticsData = dataManager.GetAnalyticsData();

        // update session length
        int sessionLength = (int)(Time.realtimeSinceStartup - sessionStartTime);
        dataManager.sessionLength += sessionLength;
        sessionStartTime += sessionLength;

        // update high score
        if (score > analyticsData.highScore) {
            analyticsData.highScore = score;
        }

        dataManager.SetAnalyticsData(analyticsData);
    }
}
