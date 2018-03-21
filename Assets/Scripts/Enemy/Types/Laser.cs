using System.Collections;
using UnityEngine;

public class Laser : Enemy {

    public GameObject line, warmup;
    public float ontime = 1f, total = 2f;

    private BoxCollider2D laserCollider;
    private Animator _animator;

    protected override void Awake() {
        laserCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        laserCollider.enabled = false;
    }

    public void Activate() {
        StartCoroutine(ManageLaser());
    }

    public override void Collided() {
    }

    private IEnumerator ManageLaser() {
        // activate warmup
        _animator.SetBool("Activate", true);
        warmup.SetActive(true);

        yield return new WaitForSeconds(ontime);

        // turn off warmup and activate laser
        _animator.SetBool("Activate", false);
        _animator.SetBool("Fire", true);
        warmup.SetActive(false);
        line.SetActive(true);
        laserCollider.enabled = true;

        yield return new WaitForSeconds(total - ontime);

        // deactivate laser
        line.SetActive(false);
        laserCollider.enabled = false;
        _animator.SetBool("Fire", false);
    }

}
