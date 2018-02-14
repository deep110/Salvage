using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Sequence", menuName = "Enemy Sequence")]
public class EnemySequence : ScriptableObject {

    public int level;

    public EnemyData[] enemies;

    [System.Serializable]
    public class EnemyData {

        public enum EnemyType {
            SPIKY_IVY, BALL, COPTER, LASER, PLATFORM_SPIKE, TANK
        }

        public EnemyType enemyType;
        public float waitTime;
        public int platformLevel;
        public bool direction;
    }
}