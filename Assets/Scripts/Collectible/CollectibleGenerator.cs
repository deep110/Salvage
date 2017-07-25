using UnityEngine;

/*
* generates coin and poweups
* on platform randomly.
*/

public class CollectibleGenerator : MonoBehaviour {

	public Vector3[] coinPositions;

	private CollectibleManager collectibleManager;
	private Transform coinsParent;

	private void Awake() {
		collectibleManager = CollectibleManager.Instance;
		coinsParent = transform.Find("coins");
	}

	private void OnEnable () {
		for (int i=0; i < coinPositions.Length; i++) {
			GameObject coin = collectibleManager.GetCoin();
			coin.transform.parent = coinsParent;
			SpawnCoin(coin, coinPositions[i]);
		}
	}

	private void SpawnCoin(GameObject coin, Vector3 localPosition) {
		coin.transform.localPosition = localPosition;
		coin.transform.rotation = Quaternion.identity;
		coin.SetActive(true);
	}
}
