/*
 * Act as top level manager for player. Gets Data from various other Managers and uses to build and control player in the game.
 * Like takes input from InputManager and passes to PlayerController, etc.
 */

using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerManager : MonoBehaviour {
    public Transform playerOne;
    public Transform playerTwo;

    private InputManager inputManager;

    private PlayerController playerOneController;
    private PlayerController playerTwoController;

    void Start() {
        inputManager = GetComponent<InputManager>();

        playerOneController = playerOne.GetComponent<PlayerController>();
        playerTwoController = playerTwo.GetComponent<PlayerController>();
    }
	
    void Update() {
        playerOneController.Move(inputManager.pointerPos.x);
        playerTwoController.Move(inputManager.pointerPos.x);

        handleJumpAndFall();
    }

    private void handleJumpAndFall() {
    	if (inputManager.pointerClick) {
    		Debug.Log("click called");
    		float playerOnePosY = playerOne.position.y;
    		float playerTwoPosY = playerTwo.position.y;

        	if(inputManager.pointerPos.y >= playerOnePosY){

        		if (playerOnePosY > playerTwoPosY){
        			// Debug.Log(playerOnePosY+"/"+playerTwoPosY+"/"+inputManager.pointerPos.y);
        			playerTwoController.Jump();
        		} else {
        			playerOneController.Jump();
        		}
        	} else {
        		if (playerOnePosY > playerTwoPosY) {
    				if (inputManager.pointerPos.y > playerOnePosY + playerTwoPosY / 2) {
    					playerTwoController.Jump();
    				} else {
    					playerOneController.Fall();
    				}
    			}else {
    				playerTwoController.Fall();
    			}
        	}
        }
    }

	
}

