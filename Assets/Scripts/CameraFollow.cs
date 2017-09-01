using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public Transform target;
	public float damping = 0.25f;
	public Vector3 distanceCorrection = new Vector3 (0, 1.0f, 0);

	private Transform _transform;
	private float offsetZ;
	private Vector3 currentVelocity;
	private bool enable;

	void Start() {
		_transform = GetComponent<Transform>();
		offsetZ = (_transform.position - target.position).z;
	}

	void LateUpdate() {
		enable = (target.localPosition.y  > 1.4f);

		if (enable) {
			Vector3 targetPos = target.position + Vector3.forward*offsetZ + distanceCorrection;
			Vector3 newPos = Vector3.SmoothDamp(_transform.position, targetPos, ref currentVelocity, damping);

			// restrict camera movement to y only
			newPos.x = 0;
			_transform.position = newPos;
		}
	}
}
