using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PowerUpManager : Singleton<PowerUpManager> {

    [System.Serializable]
    public class PowerUps {
        public GameObject shield;
        public GameObject verticalBeam;
        public GameObject horizontalBeam;
    }

    public PowerUps powerUps;

    private Transform _camera;
    private List<PowerUp> activePowerUps;
    private Dictionary<GameObject, int> powerUpWeights;

    protected override void Awake() {
        base.Awake();

        _camera = Camera.main.transform;
        powerUpWeights = new Dictionary<GameObject, int> {
            { powerUps.shield, 10 },
            { powerUps.verticalBeam, 10 },
            { powerUps.horizontalBeam, 10 }
        };

        activePowerUps = new List<PowerUp>();
    }

    private void Start() {
        StartCoroutine(GeneratePowerUps());
    }

    private void Update() {
        for (int i = 0; i < activePowerUps.Count; i++) {
            PowerUp item = activePowerUps[i];
            if (item.IsActive) {
                item.Tick();
            } else {
                activePowerUps.Remove(item);
                item.Ended();
            }
        }
    }

    public void AddActivePowerUp(PowerUp powerUp) {
        if (!powerUp.IsActive) {
            for (int i = 0; i < activePowerUps.Count; i++) {
                if (activePowerUps[i].name == powerUp.name) {
                    activePowerUps[i].ResetTime();
                    Destroy(powerUp.gameObject); // now remove this powerup
                    return;
                }
            }
            powerUp.Collected();
            activePowerUps.Add(powerUp);
        }
    }

    private void onDisable() {
        StopAllCoroutines();
    }

    private IEnumerator GeneratePowerUps() {
        var _wd = new WeightedRandomizer<GameObject>(powerUpWeights);
        while (true) {
            // wait till gameplay have started
            yield return new WaitUntil(() => GamePlayManager.Instance.getGameState() != GamePlayManager.GameState.TO_BE_STARTED);
            
            if (GamePlayManager.Instance.getGameState() == GamePlayManager.GameState.RUNNING) {
                yield return new WaitForSeconds(Random.Range(15, 22));

                GameObject selected = _wd.TakeOne();
                var powerUpPosition = new Vector3(
                    Random.Range(-2.5f, 2.5f),
                    _camera.position.y + 6f,
                    0);
                Instantiate(selected, powerUpPosition, Quaternion.identity);
            } else if (GamePlayManager.Instance.getGameState() == GamePlayManager.GameState.ENDED) {
                // empty all active powerups
                for (int i = 0; i < activePowerUps.Count; i++) {
                    PowerUp item = activePowerUps[i];
                    activePowerUps.Remove(item);
                    item.Ended();
                }
            }
        }
    }
}
