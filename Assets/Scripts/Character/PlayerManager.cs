using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerManager : Singleton <PlayerManager> {

    public Transform playerOne;
    public Transform playerTwo;

    [HideInInspector]
    public Character playerOneController;

    [HideInInspector]
    public Character playerTwoController;

    [HideInInspector]
    public bool isBeamPowerUpActive;

    private InputManager inputManager;

    protected override void Awake() {
        base.Awake();

        inputManager = GetComponent<InputManager>();

        playerOneController = playerOne.GetComponent<Character>();
        playerTwoController = playerTwo.GetComponent<Character>();
    }

    void Update() {

		switch (inputManager.GetCurrentState()) {
			case InputManager.InputState.JUMP:
				Invoke("JumpPlayerTwo", 0.12f);
				playerOneController.Jump();
				break;

			case InputManager.InputState.LEFT:
				playerOneController.Move(-1);
				playerTwoController.Move(-1);
				break;

			case InputManager.InputState.RIGHT:
				playerOneController.Move(1);
				playerTwoController.Move(1);
				break;

			case InputManager.InputState.NONE:
			case InputManager.InputState.STILL:
				playerOneController.Move(0);
				if (Mathf.Abs(playerOne.position.x - playerTwo.position.x) > 0.05f) {
					playerTwoController.Move(Mathf.Sign(playerOne.position.x - playerTwo.position.x));
				} else {
					playerTwoController.Move(0);
				}
				break;
		}
			 
    }

	private void JumpPlayerTwo() {
		playerTwoController.Jump();
	}

}
