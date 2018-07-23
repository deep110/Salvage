using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerManager : Singleton<PlayerManager> {

    public Transform playerOne;
    public Transform playerTwo;

    [HideInInspector]
    public Character playerOneController;

    [HideInInspector]
    public Character playerTwoController;

    private InputManager inputManager;
    private bool jump;
    private float jumpActiveTimeElasped;
    private bool playersCollided;


    protected override void Awake() {
        base.Awake();

        inputManager = GetComponent<InputManager>();

        playerOneController = playerOne.GetComponent<Character>();
        playerTwoController = playerTwo.GetComponent<Character>();
    }

    private void Update() {
        // keydown events read in Update and consumed in FixedUpdate
        jump |= (inputManager.GetCurrentState() == InputManager.InputState.JUMP);

        if (jump) {
            jumpActiveTimeElasped += Time.unscaledDeltaTime;
            if (jumpActiveTimeElasped > 0.3f) {
                jump = false;
                jumpActiveTimeElasped = 0;
            }
        }
    }

    private void FixedUpdate() {
        if (jump) {
            jumpPlayers();
        }

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

    public void RevivePlayers() {
        // reset positions
        playerTwo.transform.position = playerOne.transform.position - new Vector3(0, 1.76f, 0);

        playerOneController.Revive();
        playerTwoController.Revive();

        playersCollided = false;
    }

    public void PlayerCollided() {
        if (!playersCollided) {
            GamePlayManager.Instance.OnGameOver();
            playersCollided = true;
        }
    }

    private void jumpPlayers() {
        if (!playerOneController.IsJumping && !playerTwoController.IsJumping) {
            playerOneController.Jump();
            playerTwoController.Jump();
            jump = false;
            jumpActiveTimeElasped = 0;
        }
    }

}
