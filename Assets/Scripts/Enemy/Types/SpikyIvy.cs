using System.Collections;
using UnityEngine;

public class SpikyIvy : Enemy, IAttackable {

    private float ivyWidth = 0.4f;
    private BoxCollider2D ivyCollider;

    private WaitForSeconds waitTime = new WaitForSeconds(1.5f);

    protected override void Awake() {
        base.Awake();

        ivyCollider = GetComponent<BoxCollider2D>();
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        float ivyPositionY = playerPosition.y + platformLevel * PLATFORM_GAP;
        _transform.position = new Vector3(getRandomPositionX(), ivyPositionY, 0);

        StartCoroutine(collisionRoutine());
    }

    private float getRandomPositionX() {
        return Random.Range(-MAX_SCREEN_X + ivyWidth, MAX_SCREEN_X - ivyWidth);
    }

    private IEnumerator collisionRoutine() {
        ivyCollider.enabled = false;

        yield return waitTime;

        ivyCollider.enabled = true;
    }
}
