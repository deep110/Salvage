﻿using UnityEngine;

/**
* Act as top level manager for the game
* handles the game between start and till
* game is over.
*/
public class GamePlayManager : Singleton <GamePlayManager> {

	public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable () {
		EventManager.CoinCollectEvent += onCoinCollected;
	}

	void OnDisable() {
		EventManager.CoinCollectEvent -= onCoinCollected;
	}

	private void onCoinCollected() {
		score++;
	}
}