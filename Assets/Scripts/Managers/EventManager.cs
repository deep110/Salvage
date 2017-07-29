
public class EventManager {

	// game over event
	public delegate void Call();
	public static event Call GameOverEvent;

	public static void GameOver () {
		if(GameOverEvent != null)
	        GameOverEvent();
	}

	// coin collect event
	// private delegate void Call();
	public static event Call CoinCollectEvent;

	public static void CoinCollected () {
		if(CoinCollectEvent != null)
	        CoinCollectEvent();
	}
}
