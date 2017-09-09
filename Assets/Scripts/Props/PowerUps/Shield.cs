using UnityEngine;

public class Shield : PowerUp {

	private CharacterCollider playerOneCollider;
	private CharacterCollider playerTwoCollider;

	protected override void Start() {
        base.Start();

        playerOneCollider = PlayerManager.Instance.playerOne.GetComponent<CharacterCollider>();
		playerTwoCollider = PlayerManager.Instance.playerTwo.GetComponent<CharacterCollider>();
    }

	public override void Collected() {
        base.Collected();

        playerOneCollider.ActivateShield();
		playerTwoCollider.ActivateShield();
    }

	public override void Ended() {
        playerOneCollider.DeActivateShield();
		playerTwoCollider.DeActivateShield();

        base.Ended();
    }
}