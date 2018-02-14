using UnityEngine;

public interface IAttackable {
    void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel);
}