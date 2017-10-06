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
    private bool pointerClicked;
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

		switch (inputManager.getCurrentState ()) {
		case InputManager.InputState.JUMP:
			if (playerTwoPosY < playerOnePosY) {
				playerTwoController.Jump ();
			} else {
				playerOneController.Jump ();
			}
			break;

		case InputManager.InputState.LEFT:
			if (playerTwo.position.x - playerOne.position.x < 0.1f) {
				playerOneController.Move (-1);
			} else {
				playerOneController.Move (0);
			}
			playerTwoController.Move (-1);
			break;

		case InputManager.InputState.RIGHT:
			if (playerOne.position.x - playerTwo.position.x < 0.1f) {
				playerOneController.Move (1);
			} else {
				playerOneController.Move (0);
			}
			playerTwoController.Move (1);
			break;

		case InputManager.InputState.FALL:
			if (playerOnePosY > playerTwoPosY) {
				playerOneController.Fall ();
			} else if (playerTwoPosY > playerOnePosY) {
				playerTwoController.Fall ();
			}
			break;

		case InputManager.InputState.NONE:
			playerOneController.Move (0);
			playerTwoController.Move (0);
			break;
		}
    }

}
