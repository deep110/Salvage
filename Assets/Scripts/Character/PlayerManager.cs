using UnityEngine;
using System;

[RequireComponent(typeof(InputManager))]
public class PlayerManager : MonoBehaviour {

    public Transform playerOne;
    public Transform playerTwo;

    private InputManager inputManager;
    private PlayerController playerOneController;
    private PlayerController playerTwoController;

    private bool pointerClicked;
    private const float positionCorrection = -0.4f;

    void Start() {
        inputManager = GetComponent<InputManager>();

        playerOneController = playerOne.GetComponent<PlayerController>();
        playerTwoController = playerTwo.GetComponent<PlayerController>();
    }

    void Update() {
        pointerClicked |= inputManager.pointerClick;
    }

    void FixedUpdate() {
        playerOneController.Move(inputManager.pointerPos.x);
        playerTwoController.Move(inputManager.pointerPos.x);

        if (pointerClicked) {
            handleJumpAndFall();
            pointerClicked = false;
        }
    }

    private void handleJumpAndFall() {
        double playerOnePosY = Math.Round(playerOne.position.y + positionCorrection, 1);
        double playerTwoPosY = Math.Round(playerTwo.position.y + positionCorrection, 1);

        if(inputManager.pointerPos.y >= playerOnePosY){
            if (playerOnePosY > playerTwoPosY) {
                playerTwoController.Jump();
            } else {
                playerOneController.Jump();
            }
        } else {
            if (playerOnePosY > playerTwoPosY) {
                if (inputManager.pointerPos.y > (playerOnePosY + playerTwoPosY) / 2) {
                    playerTwoController.Jump();
                } else {
                    playerOneController.Fall();
                }
            } else {
                playerTwoController.Fall();
            }
        }   
    }

}

