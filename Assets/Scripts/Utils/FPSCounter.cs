using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof (Text))]
public class FPSCounter : MonoBehaviour {

    const float fpsMeasurePeriod = 0.5f;
    private int fpsAccumulator;
    private float fpsNextPeriod;
    private int currentFps;
    const string display = "FPS {0}";
    private Text text;


    void Start() {
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        text = GetComponent<Text>();
    }


    void Update() {
        // measure average frames per second
        fpsAccumulator++;
        if (Time.realtimeSinceStartup > fpsNextPeriod){
            currentFps = (int) (fpsAccumulator/fpsMeasurePeriod);
            fpsAccumulator = 0;
            fpsNextPeriod += fpsMeasurePeriod;
            text.text = string.Format(display, currentFps);
        }
    }
}

