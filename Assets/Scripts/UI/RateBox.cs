using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RateBox : Singleton<RateBox> {

    // from start of application
    public int minSessionCount = 8;

    // in minutes
    public int minSessionTime = 20;

	// in minutes
	public int postponeCooldownTime = 12 * 60;

    public bool internetRequired = true;

    public void Show() {

    }

    private void isInternet() {
        StartCoroutine(checkInternetConnection((isConnected) => {
            // handle connection status here
        }));
    }

    private IEnumerator checkInternetConnection(Action<bool> action) {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null) {
            action(false);
        } else {
            action(true);
        }
    }


}
