using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float trauma;
    public float maxOffset = 1f;

    private Transform _transform;
    private Vector3 basePosition;

    void Start() {
        _transform = GetComponent<Transform>();
    }

    void Update() {
        if (trauma > 0) {
            Vector3 offset = Shake(trauma * trauma);
            _transform.position = basePosition + offset;

            // reduce the trauma linearly
            trauma -= Time.deltaTime * 0.5f;
        } else if (trauma < 0) {
            trauma = 0;
            _transform.position = basePosition;
        }
    }

    public void Apply(float trauma) {
        if (this.trauma == 0) {
            basePosition = _transform.position;
        }
        this.trauma = trauma;
    }

    private Vector3 Shake(float shake) {
        float seed = Time.time * 10;
        Vector3 result;
        result.x = Mathf.Clamp01(Mathf.PerlinNoise(seed, 0f));
        result.y = Mathf.Clamp01(Mathf.PerlinNoise(0f, seed));
        result.z = 0;
        result = result * shake * maxOffset;
        return result;
    }
}
