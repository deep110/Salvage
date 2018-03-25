public class AnalyticsData {

	public enum RateState {
		DENIED, RATED, NONE
	};

	public int highScore;

	public RateState isRated;

	public AnalyticsData() {
		highScore = 0;

		isRated = RateState.NONE;
	}

	public override string ToString() {
		return "HighScore: " + highScore.ToString() + "\r\n" +
		"IsRated: " + isRated.ToString();
	}
}
