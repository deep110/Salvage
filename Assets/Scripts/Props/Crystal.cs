using UnityEngine;

public class Crystal : MonoBehaviour {

    public Vector2 fallVelocity = new Vector2(0, -3);

    private bool isFalling;
    private float timePassed;

    void OnEnable() {
        isFalling = false;
    }

    public void Fall() {
        if (!isFalling) {
            isFalling = true;
            GetComponent<Rigidbody2D>().velocity = fallVelocity;
            timePassed = Time.timeSinceLevelLoad;
        } else if (Time.timeSinceLevelLoad - timePassed > 0.05f) {
            Collect();
        }
    }

    public void Collect() {
        GetComponent<Transform>().GetComponentInParent<Platform>().SetCoinFall();
        EventManager.CoinCollected();
        gameObject.SetActive(false);
        isFalling = false;
    }

}
