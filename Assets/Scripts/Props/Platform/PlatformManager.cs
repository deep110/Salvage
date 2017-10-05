using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoinManager))]
public class PlatformManager : Singleton <PlatformManager> {

    public GameObject _platform;
    public float _platformGap = 1.4f;
    public float _initialPlatformPos = -1.25f;

    private ObjectPooler objectPooler;
    private Vector3 platformPosition;

    private int minPlatformIndex = -2;
    private int maxPlatformIndex;

    private Dictionary <int, GameObject> platformHashMap;

    void Start() {
        objectPooler = new ObjectPooler(_platform, 10);
        platformHashMap = new Dictionary <int, GameObject>();
        initPlatforms();
    }

    public void AddPlatform() {
        generatePlatform(maxPlatformIndex);
        maxPlatformIndex++;
    }

    public void RemovePlatform() {
        minPlatformIndex++;
        GameObject outPlatform;
        platformHashMap.TryGetValue(minPlatformIndex, out outPlatform);
        if (outPlatform != null) {
            outPlatform.SetActive(false);
            platformHashMap.Remove(minPlatformIndex);
        }
    }

    private void generatePlatform(int platformIndex) {
        float currentPlatformPos = _initialPlatformPos + platformIndex * _platformGap;
        platformPosition.Set(0, currentPlatformPos, 0);

        GameObject platform = objectPooler.SpawnInActive(platformPosition);
        platform.GetComponent<Platform>().platformIndex = platformIndex;
        platform.SetActive(true);

        platformHashMap.Add(platformIndex, platform);
    }

    private void initPlatforms () {
        for (int i = 0; i < 5; i++) {
            AddPlatform();
        }
    }
}
