using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {

    [System.Serializable]
    public class Enemies {
        public GameObject ball;
        public GameObject copter;
        public GameObject spikyIvy;
        public GameObject tank;
        public GameObject laserGrid;
        public GameObject spikes;
    }

    public Enemies enemies;
    public EnemySequence[] levelZeroSequences;
    public EnemySequence[] levelOneSequences;
    public EnemySequence[] levelTwoSequences;

    private Character playerOneController;
    private Dictionary <EnemySequence.EnemyData.EnemyType, ObjectPooler> enemyPooler;

    private bool isGameOver;
    private int platformNumber;


    void OnEnable() {
        EventManager.GameOverEvent += gameOver;
        EventManager.PlatformClimbEvent += platformClimbed;

        isGameOver = false;
        playerOneController = PlayerManager.Instance.playerOneController;

        enemyPooler = new Dictionary<EnemySequence.EnemyData.EnemyType, ObjectPooler>();
        createEnemyPooler();

        StartCoroutine(SpawnEnemies());
    }

    void OnDisable() {
        EventManager.GameOverEvent -= gameOver;
        EventManager.PlatformClimbEvent -= platformClimbed;

        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemies() {
        // wait for some time at start before spawning enemies
        yield return new WaitForSeconds(2f);

        while (!isGameOver) {
            // choose a enemy sequence at random
            EnemySequence enemySequence = getRandomSequence();

            if (enemySequence != null) {
                foreach(EnemySequence.EnemyData enemyData in enemySequence.enemies) {
                    if (enemyData.waitTime > 0) {
                        yield return new WaitForSeconds(enemyData.waitTime);
                    }
                    spawnEnemy(enemySequence.level, enemyData);
                }

                // wait time between each sequence
                yield return new WaitForSeconds(3.5f);
            }
        }
    }

    private void spawnEnemy(int sequenceLevel, EnemySequence.EnemyData enemyData) {
        GameObject enemy = enemyPooler[enemyData.enemyType].GetPooledObject();
        enemy.SetActive(true);        
        enemy.GetComponent<IAttackable>().Attack(
            sequenceLevel,
            playerOneController.GetLastStablePosition(),
            enemyData.platformLevel
        );
    }

    private EnemySequence getRandomSequence() {
        return levelZeroSequences[Random.Range(0, levelZeroSequences.Length)];
        // return levelOneSequences[Random.Range(0, levelOneSequences.Length)];
        // return levelTwoSequences[Random.Range(0, levelTwoSequences.Length)];
    }

    private void createEnemyPooler() {
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.BALL, new ObjectPooler(enemies.ball, 3));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.COPTER, new ObjectPooler(enemies.copter, 2));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.SPIKY_IVY, new ObjectPooler(enemies.spikyIvy, 3));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.TANK, new ObjectPooler(enemies.tank, 2));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.LASER, new ObjectPooler(enemies.laserGrid, 1));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.PLATFORM_SPIKE, new ObjectPooler(enemies.spikes, 2));
    }

    private void platformClimbed(int platformNumber) {
        this.platformNumber = platformNumber;
    }

    private void gameOver() {
        isGameOver = true;
    }

}
