using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoinManager))]
public class PlatformManager : Singleton <PlatformManager> {

    public GameObject _platform;
    public float _platformGap = 1.4f;
    public float _initialPlatformPos = -1.25f;

    private ObjectPooler platformPooler;
    private Vector3 platformPosition;

    private int maxPlatformIndex;

    private Queue<GameObject> activePlatforms;

    void Start() {
        platformPooler = new ObjectPooler(_platform, 8);
        activePlatforms = new Queue<GameObject>();
        initPlatforms();
    }

    public void AddPlatform() {
        platformPosition.Set(0, _initialPlatformPos + maxPlatformIndex * _platformGap, 0);

        GameObject platform = platformPooler.SpawnInActive(platformPosition);
        maxPlatformIndex++;

        platform.GetComponent<Platform>().platformIndex = maxPlatformIndex;
        platform.SetActive(true);
        activePlatforms.Enqueue(platform);
    }

    public void RemovePlatform() {
        GameObject removedPlatform = activePlatforms.Dequeue();
        removedPlatform.SetActive(false);
    }

    private void initPlatforms () {
        for (int i = 0; i < 5; i++) {
            AddPlatform();
        }
    }
}
