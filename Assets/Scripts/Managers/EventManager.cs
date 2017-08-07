
public static class EventManager {

	public delegate void Call();
	
	// game over event
	public static event Call GameOverEvent;

	public static void GameOver() {
		if(GameOverEvent != null)
	        GameOverEvent();
	}

	// coin collect event
	public static event Call CoinCollectEvent;

	public static void CoinCollected() {
		if(CoinCollectEvent != null)
	        CoinCollectEvent();
	}
}
