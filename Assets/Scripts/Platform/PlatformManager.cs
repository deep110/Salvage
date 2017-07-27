using System.Collections.Generic;
using UnityEngine;


public class PlatformManager : MonoBehaviour {

    public GameObject _platform;
    public float _platformGap = 1.45f;
    public float _initialPlatformPos = -2.0f;

    private ObjectPooler objectPooler;
    private int minPlatformIndex = -2;
    private int maxPlatformIndex;
    private int currentPlatformIndex;

    private Dictionary<int, GameObject> hashMap;

    void Start() {
        objectPooler = new ObjectPooler(_platform, 10, false);
        hashMap = new Dictionary<int, GameObject>();
        initPlatforms();
    }

    private void initPlatforms() {
        for (int i = 0; i < 6; i++) {
            AddPlatform(true);
        }
    }

    public void AddPlatform(bool up) {
        if (up) { 
            currentPlatformIndex = maxPlatformIndex;
            generatePlatform(currentPlatformIndex);
            maxPlatformIndex++;
        } else {
            currentPlatformIndex = minPlatformIndex;
            if (currentPlatformIndex >= 0) {   
                generatePlatform(currentPlatformIndex);
                minPlatformIndex--;
            } else {
                minPlatformIndex = -2;
            }
        }
    }

    public void RemovePlatform(bool up) {
        if (up) {
            maxPlatformIndex--;
            currentPlatformIndex = maxPlatformIndex;
        } else {
            minPlatformIndex++;
            currentPlatformIndex = minPlatformIndex;
        }
        GameObject outPlatform;
        hashMap.TryGetValue(currentPlatformIndex, out outPlatform);
        if (outPlatform != null) {
            outPlatform.SetActive(false);
            hashMap.Remove(currentPlatformIndex);
        }
    }

    private void generatePlatform(int platformIndex) {
        float currentPlatformPos = _initialPlatformPos + platformIndex * _platformGap;

        GameObject platform = objectPooler.Spawn(new Vector3(0, currentPlatformPos, 0));
        platform.GetComponent<CollectibleGenerator>().SetIndex(platformIndex);

        hashMap.Add(platformIndex, platform);
    }
}
