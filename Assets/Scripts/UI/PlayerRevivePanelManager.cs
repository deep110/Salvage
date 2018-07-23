using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerRevivePanelManager : MonoBehaviour {

    public void ReviveAccepted() {
        AdsManager.Instance.ShowRewardedAd(handleShowResult);
    }

    public void RevivalRejected() {
        AudioManager.Instance.PlaySound("button_click");
        // Update revival state failed to GamePlay Manager
        GamePlayManager.Instance.UpdateRevival(false);
    }

    private void handleShowResult(ShowResult result) {
        if (result == ShowResult.Finished) {
            // Update revival state success to GamePlay Manager
            GamePlayManager.Instance.UpdateRevival(true);

        } else if (result == ShowResult.Skipped || result == ShowResult.Failed) {
            RevivalRejected();
        }
    }
}