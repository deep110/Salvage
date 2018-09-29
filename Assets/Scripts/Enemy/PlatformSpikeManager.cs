using UnityEngine;

public class PlatformSpikeManager : MonoBehaviour, IAttackable {

    public float speed = 1f;
    public Transform spikeTrigger;

    private Vector2 position;

    private void Update() {
        if (speed > 0 && spikeTrigger.position.x < -4.4f) {
            position.x = 4.3f;
            spikeTrigger.position = position;
        } else if (speed < 0 && spikeTrigger.position.x > 4.3f) {
            position.x = -4.4f;
            spikeTrigger.position = position;
        }
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        transform.position = new Vector3(0, playerPosition.y + platformLevel * 1.65f, 0);

        spikeTrigger.GetComponent<Rigidbody2D>().velocity = new Vector2(getSpeed(), 0);
        position = spikeTrigger.position;
    }

    private float getSpeed() {
        float delta = this.speed/8f;
        float speed = Random.Range(this.speed - delta, this.speed + delta);

        int direction = (Random.Range(0, 1) == 0) ? -1 : 1;

        return speed * direction;      
    }
}
