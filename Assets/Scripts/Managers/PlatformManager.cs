using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : Singleton <PlatformManager> {

    public GameObject _platform;
    public float _platformGap = 1.4f;
    public float _initialPlatformPos = -1.25f;

    private ObjectPooler objectPooler;
    private Vector3 platformPosition;

    private int minPlatformIndex = -2;
    private int maxPlatformIndex;
    private int currentPlatformIndex;

    private Dictionary <int, GameObject> hashMap;

    void Start() {
        objectPooler = new ObjectPooler(_platform, 10);
        hashMap = new Dictionary <int, GameObject>();
        initPlatforms();
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
        platformPosition.Set(0, currentPlatformPos, 0);

        GameObject platform = objectPooler.SpawnInActive(platformPosition);
        platform.GetComponent<Platform>().platformIndex = platformIndex;
        platform.SetActive(true);

        hashMap.Add(platformIndex, platform);
    }

    private void initPlatforms () {
        for (int i = 0; i < 6; i++) {
            AddPlatform(true);
        }
    }
}
