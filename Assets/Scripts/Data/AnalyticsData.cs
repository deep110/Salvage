public class AnalyticsData {

	public enum RateState {
		DENIED, RATED, POSTPONED, NONE
	};

	public int highScore;
	public int coolDownTime;

	public RateState isRated;

	public AnalyticsData() {
		highScore = 0;
		coolDownTime = 0;
		isRated = RateState.NONE;
	}

	public override string ToString() {
		return "HighScore: " + highScore.ToString() + "\r\n" +
		"CoolDown Time: " + coolDownTime.ToString() + "\r\n" +
		"IsRated: " + isRated.ToString();
	}
}
