using UnityEngine;

public class CameraFollow : MonoBehaviour{
	
    public Transform target;
    public float damping = 0.25f;
	public Vector3 distanceCorrection = new Vector3 (0, 2.0f, 0);

	// private variables
	private float offsetZ;
	private Vector3 currentVelocity;

    void Start(){
		
        offsetZ = (transform.position - target.position).z;

		if (target==null)
			Debug.LogError("Target not set on Camera2DFollow.");

    }
		
	void LateUpdate(){
		
		if (target == null)
			return;

		Vector3 targetPos = target.position + Vector3.forward*offsetZ + distanceCorrection;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, damping);

		// restrict camera movement to y only
		newPos.x = 0;
        transform.position = newPos;
    }
}

