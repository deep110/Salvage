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
        pointerClicked |= inputManager.pointerClick;
    }

    void FixedUpdate() {
        playerOneController.Move(inputManager.pointerPos.x);
        playerTwoController.Move(inputManager.pointerPos.x);

        if (pointerClicked && !isBeamPowerUpActive) {
            handleJumpAndFall();
            pointerClicked = false;
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

