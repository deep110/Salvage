using System.Collections.Generic;
using UnityEngine;


public class PlatformManager : MonoBehaviour {

    public GameObject _platform;
    public float _platformGap = 1.45f;
    public float _initialPlatformPos = -2.0f;

    private ObjectPooler objectPooler;
    private Vector3 platformPosition;
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
        platformPosition.Set(0, currentPlatformPos, 0);

        GameObject platform = objectPooler.GetPooledObject();
        Transform platformTransform = platform.transform;
        platformTransform.GetChild(1).GetComponent<CollectibleGenerator>().SetIndex(platformIndex);
        platformTransform.position = platformPosition;
        platformTransform.rotation = Quaternion.identity;
        platform.SetActive(true);

        hashMap.Add(platformIndex, platform);
    }
}
