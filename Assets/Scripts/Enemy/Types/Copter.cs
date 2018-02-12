using UnityEngine;

public class Copter : Enemy {

    public Vector2 _velocity;
    private Rigidbody2D _rigidbody;

    protected override void Awake() {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        _velocity.Set(0, -1.3f);
        _rigidbody.velocity = _velocity;
    }
}
