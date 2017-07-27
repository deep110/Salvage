using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : Singleton<CollectibleManager> {

	public GameObject coin;

	private ObjectPooler coinObjectPooler;
	private Dictionary<int, string> coinHashMap;

	void Start () {
		coinObjectPooler = new ObjectPooler(coin, 30);
		coinHashMap = new Dictionary<int, string>();
	}

	public GameObject GetCoin() {
		return coinObjectPooler.GetPooledObject();
	}

	public string GetCoinData(int coinsIndex) {
		string coinData;
		coinHashMap.TryGetValue(coinsIndex, out coinData);
		return coinData;
	}

	public void SaveCoinData(int coinsIndex, string data) {
		coinHashMap[coinsIndex] = data;
	}

}
