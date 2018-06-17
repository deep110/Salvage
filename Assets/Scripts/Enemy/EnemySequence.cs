using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Sequence", menuName = "Enemy Sequence")]
public class EnemySequence : ScriptableObject {

    public int level;

    public EnemyData[] enemies;

    [System.Serializable]
    public class EnemyData {

        public EnemyType enemyType;
        public float waitTime;
        public int platformLevel;
    }
}