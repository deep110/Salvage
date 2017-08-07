using UnityEngine;

public class EnemyManager : Singleton <EnemyManager> {

	[System.Serializable]
	public class Enemies {
		public GameObject ball;
	}

	public Enemies enemies;

	private ObjectPooler ballObjectPooler;

	void Awake () {
		ballObjectPooler = new ObjectPooler(enemies.ball, 4);
	}


	// void Start () {
		
	// }
	

	// void Update () {
		
	// }
}
