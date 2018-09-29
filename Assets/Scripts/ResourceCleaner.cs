using UnityEngine;

/*
* cleans up resorce from screen like coins
* or enemies.
*/
public class ResourceCleaner : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Crystal")) {
            other.gameObject.SetActive(false);
        } else if (other.gameObject.CompareTag("PowerUp")) {
            if (!other.gameObject.GetComponent<PowerUp>().IsActive) {
                Destroy(other.gameObject);
            }
        } else if (other.gameObject.CompareTag("Enemy")) {
            Transform ps = other.transform.parent;
            if (ps != null) {
                ps.parent.gameObject.SetActive(false);
            } else {
                other.gameObject.SetActive(false);
            }
        }
    }
}
