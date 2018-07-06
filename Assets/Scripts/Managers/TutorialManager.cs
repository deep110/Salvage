using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputManager))]
public class TutorialManager : MonoBehaviour {

    public Transform playerOne;
    public Transform playerTwo;
    public TMP_Text tutorialText;

    [Header("Step One")]
    public GameObject playerFinalPosition;

    private Character playerOneController;
    private Character playerTwoController;
    private InputManager inputManager;

    private bool isTutorialRunning;
    private int tutorialStep;


    private void Start() {
        inputManager = GetComponent<InputManager>();
        playerOneController = playerOne.GetComponent<Character>();
        playerTwoController = playerTwo.GetComponent<Character>();
        tutorialStep = 1;

        StartCoroutine(runTutorial());
    }

    private IEnumerator runTutorial() {
        isTutorialRunning = true;
        while (isTutorialRunning) {
            switch (tutorialStep) {
                case 1:
                    if (checkIsDragComplete()) {
                        tutorialStep++;
                        playerFinalPosition.GetComponent<ParticleSystem>().Stop();
                        tutorialText.text = "Awesome !!";
                        yield return new WaitForSeconds(2f);
                        tutorialText.text = "Tap to jump!";
                    }
                    break;

                case 2:
                    if (jumpPlayers()) {
                        tutorialStep++;
                        tutorialText.text = "Awesome !!";
                        yield return new WaitForSeconds(2f);
                        tutorialText.text = "Knock the crystal to collect";
                    }
                    break;

                case 3:
                    yield return new WaitForSeconds(5f);
                    tutorialStep++;
                    tutorialText.text = "You Rock !!";
                    yield return new WaitForSeconds(2f);
                    break;

                default:
                    isTutorialRunning = false;
                    loadGameScene();
                    break;
            }

            yield return null;
        }
    }

    private bool checkIsDragComplete() {
        return (playerOne.position.x > playerFinalPosition.transform.position.x);
    }

    private void FixedUpdate() {
        // move player one
        switch (inputManager.GetCurrentState()) {
            case InputManager.InputState.MOVE:
                playerOneController.Move(inputManager.GetDeltaX());
                break;

            case InputManager.InputState.NONE:
            case InputManager.InputState.STILL:
                playerOneController.Move(0);
                break;
        }

        // make playerTwo follow player one
        float diff = (playerOne.position.x - playerTwo.position.x);
        if (Mathf.Abs(diff) > 0.08f) {
            playerTwoController.Move(diff);
        } else {
            playerTwoController.Move(0);
        }

        playerOneController.Hover();
        playerTwoController.Hover();
    }

    private bool jumpPlayers() {
        bool jump = (inputManager.GetCurrentState() == InputManager.InputState.JUMP);

        if (jump && !playerOneController.IsJumping && !playerTwoController.IsJumping) {
            playerOneController.Jump();
            playerTwoController.Jump();
            return true;
        }
        return false;
    }

    private void loadGameScene() {
        SceneManager.LoadScene(2);
    }
}
