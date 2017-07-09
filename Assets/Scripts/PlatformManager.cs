using UnityEngine;

/*
 * genrates platform
 * use object pool
 */
public class PlatformManager : MonoBehaviour
{

    public GameObject platform;
    public float platformGap = 1.6f;
    public float initialPlatformPos = -2.0f;

    private ObjectPooler objectPooler;
    private int minPlatformIndex;
    private int maxPlatformIndex;
    private float currentPlatformPos;

    private CameraFollow cameraFollow;
    private Transform targetPlayer;
    private float targetPosY;

    void Start()
    {
        // initialize object pooler
        objectPooler = new ObjectPooler(platform, 8);
        initPlatforms();

        // get camera transform
        targetPlayer = GetComponent<Transform>().parent.GetComponent<CameraFollow>().target;
        targetPosY = targetPlayer.position.y;
    }

    void Update()
    {
        targetPosY = targetPlayer.position.y;
//		Debug.Log (targetPosY + "/" + currentPlatformPos);
    }

    private void initPlatforms()
    {
        for (int i = 0; i < 5; i++)
        {
            generatePlatform(true);
        }
    }

    private void generatePlatform(bool up)
    {
        if (up)
        {
            currentPlatformPos = initialPlatformPos + maxPlatformIndex * platformGap;
            maxPlatformIndex++;
        }
        else
        {
            currentPlatformPos = initialPlatformPos + minPlatformIndex * platformGap;
            minPlatformIndex--;
        }
        objectPooler.Spawn(new Vector3(0, currentPlatformPos, 0));
    }
}
