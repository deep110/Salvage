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
    private int randomizerLevel = 0;
    private WeightedRandomizer<int> weightedRandomizer;


    void OnEnable() {
        EventManager.GameOverEvent += gameOver;
        EventManager.PlatformClimbEvent += platformClimbed;

        playerOneController = PlayerManager.Instance.playerOneController;

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
                float waitSequenceTime = Random.Range(2.2f, 2.5f);
                if (enemySequence.level == 1) {
                    waitSequenceTime -= 0.5f;
                } else if (enemySequence.level == 2) {
                    waitSequenceTime -= Random.Range(0.5f, 0.8f);
                }
                yield return new WaitForSeconds(waitSequenceTime);
            }
        }
    }

    private void spawnEnemy(int sequenceLevel, EnemySequence.EnemyData enemyData) {
        GameObject enemy = enemyPooler[enemyData.enemyType].GetPooledObject();
        enemy.GetComponent<IAttackable>().Attack(
            sequenceLevel,
            playerOneController.GetLastStablePosition(),
            enemyData.platformLevel
        );
        enemy.SetActive(true);        
    }

    private EnemySequence getRandomSequence() {
        EnemySequence enemySequence = null;
        if (platformNumber <= 30) {
            enemySequence = levelZeroSequences[Random.Range(0, levelZeroSequences.Length)];
        } else if (platformNumber <= 60) {
            updateRandomizer(1);
            int seqLevel = weightedRandomizer.TakeOne();
            if (seqLevel == 1) {
                enemySequence = levelOneSequences[Random.Range(0, levelOneSequences.Length)];
            } else {
                enemySequence = levelZeroSequences[Random.Range(0, levelZeroSequences.Length)];
                enemySequence.level = 1;
            }
        } else {
            updateRandomizer(2);
            int seqLevel = weightedRandomizer.TakeOne();
            if (seqLevel == 2) {
                enemySequence = levelTwoSequences[Random.Range(0, levelTwoSequences.Length)];
            } else if (seqLevel == 1) {
                enemySequence = levelOneSequences[Random.Range(0, levelOneSequences.Length)];
                enemySequence.level = 2;
            } else {
                enemySequence = levelZeroSequences[Random.Range(0, levelZeroSequences.Length)];
                enemySequence.level = 2;
            }
        }

        return enemySequence;
    }

    private void createEnemyPooler() {
        enemyPooler = new Dictionary<EnemySequence.EnemyData.EnemyType, ObjectPooler>();
        
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.BALL, new ObjectPooler(enemies.ball, 3));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.COPTER, new ObjectPooler(enemies.copter, 2));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.SPIKY_IVY, new ObjectPooler(enemies.spikyIvy, 3));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.TANK, new ObjectPooler(enemies.tank, 2));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.LASER, new ObjectPooler(enemies.laserGrid, 1));
        enemyPooler.Add(EnemySequence.EnemyData.EnemyType.PLATFORM_SPIKE, new ObjectPooler(enemies.spikes, 2));
    }

    private void updateRandomizer(int currentLevel) {
        if (currentLevel == 1 && randomizerLevel != 1) {
            weightedRandomizer = new WeightedRandomizer<int>(new Dictionary<int, int> {
                {0, 60}, {1, 40}
            });
            randomizerLevel = 1;
        } else if (currentLevel == 2 && randomizerLevel != 2) {
            weightedRandomizer = new WeightedRandomizer<int>(new Dictionary<int, int> {
                {0, 45}, {1, 30}, {2, 25}
            });
            randomizerLevel = 2;
        }
    }

    private void platformClimbed(int platformNumber) {
        this.platformNumber = platformNumber;
    }

    private void gameOver() {
        isGameOver = true;
    }

}
