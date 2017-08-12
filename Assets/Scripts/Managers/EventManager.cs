
public static class EventManager {

	public delegate void Call();
	public delegate void CallArgs<T>(T arg0);
	
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

	// platform climb event
	public static event CallArgs <int> PlatformClimbEvent;

	public static void PlatformClimbed(int platformNumber) {
		if(PlatformClimbEvent != null)
	        PlatformClimbEvent(platformNumber);
	}
}
