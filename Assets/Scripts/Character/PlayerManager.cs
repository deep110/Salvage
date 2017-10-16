using UnityEngine;
using System;

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
    private const float positionCorrection = -0.643f;

    protected override void Awake() {
        base.Awake();

        inputManager = GetComponent<InputManager>();

        playerOneController = playerOne.GetComponent<Character>();
        playerTwoController = playerTwo.GetComponent<Character>();
    }

    void Update() {

		double playerOnePosY = Math.Round(playerOne.position.y + positionCorrection, 1);
		double playerTwoPosY = Math.Round(playerTwo.position.y + positionCorrection, 1);

		switch (inputManager.GetCurrentState ()) {
			case InputManager.InputState.JUMP:
				Invoke ("JumpPlayerTwo", 0.2f);
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

			case InputManager.InputState.FALL:
				if (playerOnePosY > playerTwoPosY) {
					playerOneController.Fall();
				} else if (playerTwoPosY > playerOnePosY) {
					playerTwoController.Fall();
				}
				break;

			case InputManager.InputState.NONE:
				playerOneController.Move(0);
				if (playerOne.position.x - playerTwo.position.x > 0.1f) {
					playerTwoController.Move(1);
				} else if (playerTwo.position.x - playerOne.position.x > 0.1f) {
					playerTwoController.Move(-1);
				} else {
					playerTwoController.Move(0);
				}
				break;

			case InputManager.InputState.STILL:
				playerOneController.Move(0);
				if (playerOne.position.x - playerTwo.position.x > 0.1f) {
					playerTwoController.Move(1);
				} else if (playerTwo.position.x - playerOne.position.x > 0.1f) {
					playerTwoController.Move(-1);
				} else {
					playerTwoController.Move(0);
				}
				break;
		}
			 
    }

	void JumpPlayerTwo() {
		playerTwoController.Jump();
	}

}
