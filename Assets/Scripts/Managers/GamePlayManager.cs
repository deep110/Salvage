using UnityEngine;

/**
* Act as top level manager for the game
* handles the game between start and till
* game is over.
*/
public class GamePlayManager : Singleton<GamePlayManager> {

    public int score;
    public int platformsClimbed;

    private UIManager uiManager;

    void Start() {
        Time.timeScale = 1;
        uiManager = UIManager.Instance;

        EventManager.CrystalCollectEvent += onCrystalCollected;
        EventManager.PlatformClimbEvent += onPlatformClimbed;
        EventManager.GameStateEvent += onGameOver;
        EventManager.PlatformClearEvent += onPlatformClear;
    }

    void OnDisable() {
        EventManager.CrystalCollectEvent -= onCrystalCollected;
        EventManager.PlatformClimbEvent -= onPlatformClimbed;
        EventManager.GameStateEvent -= onGameOver;
        EventManager.PlatformClearEvent -= onPlatformClear;
    }

    private void onCrystalCollected() {
        score++;
        uiManager.UpdateScoreText(score);
    }

    private void onPlatformClimbed(int platforms) {
        platformsClimbed = platforms;
        // uiManager.UpdatePlatformText(platforms);
    }

    private void onGameOver(bool isOver) {
        if (isOver) {
            // pause the game
            Time.timeScale = 0;

            // show the gameOver dialog
            uiManager.setPlayerRevivePanelState(true);
        } else {
            // revival is called
            Time.timeScale = 1;
            // disable the revival dialog
            uiManager.setPlayerRevivePanelState(false);
        }
    }

    private void onPlatformClear() {
        //increase the score by 3
        score += 3;
        //display the platform clear panel
        uiManager.ShowPlatformClear();
        //update the ui score
        uiManager.UpdateScoreText(score);
    }
}
