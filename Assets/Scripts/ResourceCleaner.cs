﻿using UnityEngine;

/*
* cleans up resorce from screen like coins
* or enemies.
*/
public class ResourceCleaner : MonoBehaviour {
	
	void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("Enemy")) {
            other.gameObject.SetActive(false);
        } else if (other.gameObject.CompareTag("PowerUp")) {
        	Destroy(other.gameObject);
        }
    }
}