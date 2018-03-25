using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerRevivePanelManager : MonoBehaviour {

    public void ReviveAccepted() {
        AdsManager.Instance.ShowRewardedAd(handleShowResult);
    }

    public void ReviveRejected() {
        // disable this dialog
        gameObject.SetActive(false);

        // show gameOver Dialog
        UIManager.Instance.setGameOverPanelState(true);
    }

    private void handleShowResult(ShowResult result) {
        if (result == ShowResult.Finished) {
            // Call Revive
            EventManager.GameStateChange(false);

        } else if (result == ShowResult.Skipped || result == ShowResult.Failed) {
            Debug.LogWarning(result.ToString());
            
            ReviveRejected();
        }
    }
}