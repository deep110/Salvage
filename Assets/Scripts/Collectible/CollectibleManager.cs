using UnityEngine;

public class CollectibleManager : Singleton<CollectibleManager> {

	public GameObject coin;

	private ObjectPooler coinObjectPooler;

	void Start () {
		coinObjectPooler = new ObjectPooler(coin, 30);
	}

	public GameObject GetCoin() {
		return coinObjectPooler.GetPooledObject();
	}

}
