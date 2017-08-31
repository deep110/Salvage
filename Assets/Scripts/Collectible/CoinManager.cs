using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton <CoinManager> {

    public GameObject coin;

    private ObjectPooler coinObjectPooler;
    private Dictionary <int, bool[]> coinHashMap;

    protected override void Awake() {
        base.Awake();

        coinObjectPooler = new ObjectPooler(coin, 30);
        coinHashMap = new Dictionary <int, bool[]>();
    }

    public GameObject GetCoin() {
        return coinObjectPooler.GetPooledObject();
    }

    public bool[] GetCoinData(int platformIndex) {
        bool[] coinData;
        coinHashMap.TryGetValue(platformIndex, out coinData);

        if (coinData == null)
            coinData = new [] { true, true, true, true };
        return coinData;
    }

    public void SaveCoinData(int platformIndex, bool[] data) {
        coinHashMap[platformIndex] = data;
    }

}
