using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float damping = 0.25f;
    public Vector3 distanceCorrection = new Vector3(0, 1.0f, 0);

    private Transform _transform;
    private float offsetZ;
    private Vector3 currentVelocity;

    void Start() {
        _transform = GetComponent<Transform>();
        offsetZ = (_transform.position - target.position).z;
    }

    void LateUpdate() {

        if (target.localPosition.y > 1.4f) {
            Vector3 targetPos = target.position + Vector3.forward * offsetZ + distanceCorrection;
            Vector3 newPos = Vector3.SmoothDamp(_transform.position, targetPos, ref currentVelocity, damping);

            // restrict camera movement to y only, do not change x
            newPos.x = _transform.position.x;
            _transform.position = newPos;
        }
    }
}

