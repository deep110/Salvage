﻿using UnityEngine;

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
        gameState = GameState.RUNNING;
        uiManager = UIManager.Instance;
        dataManager = DataManager.Instance;
        sessionStartTime = Time.realtimeSinceStartup;

        EventManager.CrystalCollectEvent += onCrystalCollected;
        EventManager.PlatformClimbEvent += onPlatformClimbed;
        EventManager.PlatformClearEvent += onPlatformClear;
    }

    private void Start() {
        AudioManager.Instance.PlaySound("gameplay");
    }

    private void OnDisable() {
        EventManager.CrystalCollectEvent -= onCrystalCollected;
        EventManager.PlatformClimbEvent -= onPlatformClimbed;
        EventManager.PlatformClearEvent -= onPlatformClear;
    }

    public void UpdateRevival(bool revivalAccepted) {
        if (revivalAccepted) {
            Time.timeScale = 1;
            gameState = GameState.RUNNING;
        } else {
            // end the game
            makeGameEnd();
        }
    }

    public void UpdateTutorial(bool isShowingTutorial) {
        if (isShowingTutorial) {
            gameState = GameState.PAUSED;
        } else {
            gameState = GameState.RUNNING;
        }
    }

    public void OnGameOver() {
        // pause the game
        Time.timeScale = 0;
        // update analytics to local storage
        updateAnalytics();

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
        AudioManager.Instance.StopSound("gameplay");

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
