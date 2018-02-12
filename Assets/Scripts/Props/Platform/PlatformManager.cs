using UnityEngine;

public class PlatformManager : Singleton<PlatformManager> {

    public GameObject _platform;
    public GameObject _coin;

    public float _platformGap = 1.65f;
    public float _initialPlatformPos = -1f;

    private ObjectPooler platformPooler;
    private ObjectPooler coinPooler;
    private Vector3 platformPosition;

    private int maxPlatformIndex;

    void Start() {
        platformPooler = new ObjectPooler(_platform, 8);
        coinPooler = new ObjectPooler(_coin, 32);
        initPlatforms();
    }

    public void AddPlatform() {
        platformPosition.Set(0, _initialPlatformPos + maxPlatformIndex * _platformGap, 0);

        GameObject platform = platformPooler.SpawnInActive(platformPosition);
        maxPlatformIndex++;

        platform.GetComponent<Platform>().platformIndex = maxPlatformIndex;
        platform.SetActive(true);
    }

    public GameObject GetCoin() {
        return coinPooler.GetPooledObject();
    }

    private void initPlatforms() {
        for (int i = 0; i < 5; i++) {
            AddPlatform();
        }
    }
}
