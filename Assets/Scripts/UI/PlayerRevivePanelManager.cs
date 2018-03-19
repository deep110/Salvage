using UnityEngine;

public class PlayerRevivePanelManager : MonoBehaviour {

    public void ReviveAccepted() {
        // TODO:
        // Connect with AdsManager
        // Show a video ad
        // Revive only if full ad is seen

        // Call Revive
        EventManager.GameStateChange(false);
    }

    public void ReviveRejected() {
        // disable this dialog
        gameObject.SetActive(false);

        // show gameOver Dialog
        UIManager.Instance.setGameOverPanelState(true);
    }
}