using System.Collections;
using UnityEngine;

public class Laser : Enemy {

    public GameObject line, warmup;
    public float ontime = 1f, total = 2f;

    private BoxCollider2D laserCollider;
    private Animator _animator;

    private WaitForSeconds wfsOnTime;
    private WaitForSeconds wfsDeltaTime;
    private const string ANIMATOR_ACTIVATE = "Activate";
    private const string ANIMATOR_FIRE = "Fire";

    protected override void Awake() {
        laserCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        laserCollider.enabled = false;

        wfsOnTime = new WaitForSeconds(ontime);
        wfsDeltaTime = new WaitForSeconds(total - ontime);
    }

    public void Activate() {
        StartCoroutine(ManageLaser());
    }

    public override void Collided() {
    }

    private IEnumerator ManageLaser() {
        // activate warmup
        _animator.SetBool(ANIMATOR_ACTIVATE, true);
        warmup.SetActive(true);

        yield return wfsOnTime;

        // turn off warmup and activate laser
        _animator.SetBool(ANIMATOR_ACTIVATE, false);
        _animator.SetBool(ANIMATOR_FIRE, true);
        warmup.SetActive(false);
        line.SetActive(true);
        laserCollider.enabled = true;

        yield return wfsDeltaTime;

        // deactivate laser
        line.SetActive(false);
        laserCollider.enabled = false;
        _animator.SetBool(ANIMATOR_FIRE, false);
    }

}
