using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerManager : MonoBehaviour {
    public Transform playerOne;
    public Transform playerTwo;

    private InputManager inputManager;

    private PlayerController playerOneController;
    private PlayerController playerTwoController;

    private bool pointerClicked;

    void Start() {
        inputManager = GetComponent<InputManager>();

        playerOneController = playerOne.GetComponent<PlayerController>();
        playerTwoController = playerTwo.GetComponent<PlayerController>();
    }

    void Update() {
    	if (inputManager.pointerClick){
    		pointerClicked = true;
    	}
    }
	
    void FixedUpdate() {
        playerOneController.Move(inputManager.pointerPos.x);
        playerTwoController.Move(inputManager.pointerPos.x);

        handleJumpAndFall();
    }

    private void handleJumpAndFall() {
    	if (pointerClicked) {
    		pointerClicked = false;
    		float playerOnePosY = playerOne.position.y;
    		float playerTwoPosY = playerTwo.position.y;

        	if(inputManager.pointerPos.y >= playerOnePosY){

        		if (playerOnePosY > playerTwoPosY){
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

