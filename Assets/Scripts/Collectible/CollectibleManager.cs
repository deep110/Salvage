using UnityEngine;

public class CoinManager : MonoBehaviour {

	public GameObject coin;

	private ObjectPooler coinObjectPooler;

	void Start () {
		coinObjectPooler = new ObjectPooler(coin, 50);
	}

	public void SpawnCoin(Vector3 position) {
		coinObjectPooler.Spawn(position);
	}
	
	
}
