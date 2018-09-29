using UnityEngine;

public class ShockwaveScript : MonoBehaviour {

	void Update () {
        transform.localScale += new Vector3(0.05f, 0.05f, 0);
        if(transform.localScale.x > 4f) {
            gameObject.SetActive(false);
        }
	}
}
