using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public bool isActive = true;

    protected Transform _transform;
    protected float PLATFORM_GAP = 1.65f;
    protected float MAX_SCREEN_X = 3f;

    protected virtual void Awake() {
        PLATFORM_GAP = PlatformManager._platformGap;
        _transform = GetComponent<Transform>();
    }

    public virtual void Collided() {
        // play enemy die animation
        // play sound
        gameObject.SetActive(false);
    }

}
