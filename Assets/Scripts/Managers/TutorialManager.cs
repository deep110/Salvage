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
    public GameObject playerRightPosition;

    [Header("Step Two")]
    public GameObject playerLeftPosition;

    [Header("Step Four")]
    public GameObject crystal;

    private Character playerOneController;
    private Character playerTwoController;
    private InputManager inputManager;

    private bool isTutorialRunning;
    private int tutorialStep;
    private bool crystalCollected;


    private void Start() {
        inputManager = GetComponent<InputManager>();
        playerOneController = playerOne.GetComponent<Character>();
        playerTwoController = playerTwo.GetComponent<Character>();

        StartCoroutine(runTutorial());
    }

    private IEnumerator runTutorial() {
        // prepare things for runnning first tutorial
        tutorialStep = 1;
        isTutorialRunning = true;
        playerRightPosition.SetActive(true);

        while (isTutorialRunning) {
            switch (tutorialStep) {
                case 1:
                    if (checkIsRightDragComplete()) {
                        tutorialStep++;
                        playerRightPosition.SetActive(false);
                        tutorialText.text = "Awesome !!";
                        yield return new WaitForSeconds(1f);

                        // make things ready for next step
                        tutorialText.text = "Drag the finger to move\nleft";
                        playerLeftPosition.SetActive(true);
                    }
                    break;

                case 2:
                    if (checkIsLeftDragComplete()) {
                        tutorialStep++;
                        playerLeftPosition.SetActive(false);
                        tutorialText.text = "Awesome !!";
                        yield return new WaitForSeconds(1f);

                        // make things ready for next step
                        tutorialText.text = "Now\nTap to jump!";
                    }
                    break;

                case 3:
                    if (jumpPlayers()) {
                        tutorialStep++;
                        tutorialText.text = "Awesome !!";
                        yield return new WaitForSeconds(1f);

                        // make things ready for next step
                        tutorialText.text = "Knock the crystal to collect";
                        EventManager.CrystalCollectEvent += onCrystalCollected;
                        crystal.SetActive(true);
                    }
                    break;

                case 4:
                    yield return new WaitUntil(() => crystalCollected);
                    tutorialStep++;
                    tutorialText.text = "You Rock !!";
                    EventManager.CrystalCollectEvent -= onCrystalCollected;
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

    private bool checkIsRightDragComplete() {
        return (playerOne.position.x > playerRightPosition.transform.position.x);
    }

    private bool checkIsLeftDragComplete() {
        return (playerOne.position.x < playerLeftPosition.transform.position.x);
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

    private void onCrystalCollected() {
        crystalCollected = true;
    }

    private void loadGameScene() {
        SceneManager.LoadScene(3);
    }
}
