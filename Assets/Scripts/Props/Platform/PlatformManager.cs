using UnityEngine;

public class PlatformManager : Singleton<PlatformManager> {

    public GameObject _platform;
    public GameObject _coin;
    public Transform _groundPosition;

    public const float _platformGap = 1.65f;

    private ObjectPooler platformPooler;
    private ObjectPooler coinPooler;
    private Vector3 platformPosition;
    private float initialPlatformPos;    

    private int maxPlatformIndex;

    void Start() {
        platformPooler = new ObjectPooler(_platform, 7);
        coinPooler = new ObjectPooler(_coin, 28);
        initialPlatformPos = _groundPosition.position.y + _platformGap;

        // initialize the starting platforms
        initPlatforms();
    }

    public void AddPlatform() {
        platformPosition.Set(0, initialPlatformPos + maxPlatformIndex * _platformGap, 0);

        GameObject platform = platformPooler.SpawnInActive(platformPosition);
        maxPlatformIndex++;

        platform.GetComponent<Platform>().platformIndex = maxPlatformIndex;
        platform.SetActive(true);
    }

    public GameObject GetCrystal() {
        return coinPooler.GetPooledObject();
    }

    private void initPlatforms() {
        for (int i = 0; i < 5; i++) {
            AddPlatform();
        }
    }
}
