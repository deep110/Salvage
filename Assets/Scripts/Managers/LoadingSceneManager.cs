using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    public float splashDuration = 1.5f;

    private IEnumerator Start() {
        yield return new WaitForSeconds(splashDuration);

        SceneManager.LoadScene(1);
    }
}
