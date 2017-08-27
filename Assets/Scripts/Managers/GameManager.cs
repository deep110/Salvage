using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton <GameManager> {

	private IEnumerator Start () {
		LocalizationManager.Instance.LoadLocalizedText("en");

		while (!LocalizationManager.Instance.IsReady()) {
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(1);
    }

}
