using System.Collections;
using UnityEngine;
using TMPro;

public class RateBox : Singleton<RateBox> {

    [Header("Display Conditions")]
    // from start of application
    public int minSessionCount = 5;
    public int minSessionTime = 12; // in minutes
    public int postponeCooldownTime = 15; // in minutes

    [Header("RateBox UI")]
    public string title;
    [TextArea]
    public string message;

    [Header("Store Urls")]
    public string playStoreId;

    private GameObject ui;
    private TMP_Text titleTextUI;
    private TMP_Text messageTextUI;
    private bool isInternet;
    private DataManager dataManager;

    private void Start() {
        StartCoroutine(checkInternetConnection());
        dataManager = DataManager.Instance;

        ui = transform.GetChild(0).gameObject;
        titleTextUI = ui.transform.GetChild(1).GetComponent<TMP_Text>();
        messageTextUI = ui.transform.GetChild(2).GetComponent<TMP_Text>();
        titleTextUI.text = title;
        messageTextUI.text = message;

        ui.SetActive(false);
    }

    public bool Show() {
        if (canShow() && isInternet && dataManager.sessionsCount >= minSessionCount &&
                dataManager.sessionLength >= minSessionTime * 60) {
            ui.SetActive(true);

            return true;
        }
        return false;
    }

    public void OnRateAccepted() {
        AudioManager.Instance.PlaySound("button_click");
        // open playstore
        Application.OpenURL("market://details?id=" + playStoreId);

        // update rate state
        AnalyticsData analyticsData = dataManager.GetAnalyticsData();
        analyticsData.isRated = AnalyticsData.RateState.RATED;
        dataManager.SetAnalyticsData(analyticsData);

        ui.SetActive(false);
    }

    public void OnRatePostponed() {
        AudioManager.Instance.PlaySound("button_click");
        dataManager.ResetCoolDownTime();

        // update rate state
        AnalyticsData analyticsData = dataManager.GetAnalyticsData();
        analyticsData.isRated = AnalyticsData.RateState.POSTPONED;
        analyticsData.coolDownTime = 0;
        dataManager.SetAnalyticsData(analyticsData);

        ui.SetActive(false);
    }

    public void OnRateRejected() {
        AudioManager.Instance.PlaySound("button_click");
        AnalyticsData analyticsData = dataManager.GetAnalyticsData();
        analyticsData.isRated = AnalyticsData.RateState.DENIED;
        dataManager.SetAnalyticsData(analyticsData);

        ui.SetActive(false);
    }

    private IEnumerator checkInternetConnection() {
        WWW www = new WWW("http://google.com");
        yield return www;
        isInternet = (www.error == null);
    }

    private bool canShow() {
        AnalyticsData.RateState rateState = dataManager.GetAnalyticsData().isRated;
        bool status = false;

        switch (rateState) {
            case AnalyticsData.RateState.NONE:
                status = true;
                break;
            
            case AnalyticsData.RateState.POSTPONED:
                status = dataManager.GetCoolDownTime() >= postponeCooldownTime * 60;
                break;
        }
        return status;
    }


}
