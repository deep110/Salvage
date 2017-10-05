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
			playerOneController.Move (-1);
			playerTwoController.Move (-1);
			break;

		case InputManager.InputState.RIGHT:
			playerOneController.Move (1);
			playerTwoController.Move (1);
			break;

		case InputManager.InputState.NONE:
			playerOneController.Move (0);
			playerTwoController.Move (0);
			break;
		}
    }

    private void handleJumpAndFall() {
        double playerOnePosY = Math.Round(playerOne.position.y + positionCorrection, 1);
        double playerTwoPosY = Math.Round(playerTwo.position.y + positionCorrection, 1);

		if (inputManager.pointerPos.y >= playerOnePosY && inputManager.pointerPos.y >= playerTwoPosY) {
            if (playerOnePosY > playerTwoPosY) {
                playerTwoController.Jump();
            } else {
                playerOneController.Jump();
            }
		} else {
            if (playerOnePosY > playerTwoPosY) {
                playerOneController.Fall();
			} else if(playerOnePosY < playerTwoPosY){
                playerTwoController.Fall();
            }
        }   
    }
}

