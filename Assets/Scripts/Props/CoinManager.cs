using UnityEngine;

public class CoinManager : Singleton <CoinManager> {

    public GameObject coin;

    private ObjectPooler coinObjectPooler;

    protected override void Awake() {
        base.Awake();

        coinObjectPooler = new ObjectPooler(coin, 30);
    }

    public GameObject GetCoin() {
        return coinObjectPooler.GetPooledObject();
    }

}
