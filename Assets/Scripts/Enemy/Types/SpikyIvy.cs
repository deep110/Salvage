using UnityEngine;

public class SpikyIvy : Enemy, IAttackable {

    private float ivyWidth = 0.4f;

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        float ivyPositionY = playerPosition.y + platformLevel * PLATFORM_GAP;

        _transform.position = new Vector3(getRandomPositionX(), ivyPositionY, 0);
    }

    private float getRandomPositionX() {
        return Random.Range(-MAX_SCREEN_X + ivyWidth, MAX_SCREEN_X - ivyWidth);
    }
}
