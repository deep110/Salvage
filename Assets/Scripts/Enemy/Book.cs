using UnityEngine;

public class Book : Enemy {

	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2f);
	}
}
