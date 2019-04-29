using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("States")]
    public bool CanMove;
    public bool CanInputMove;

    [Header("Speed")]
    public float Speed = 5f;
    public float Acceleration = 0.1f;

    [Header("Damage")]
    public float Damage = 1f;

    [Header("Effects")]
    public ParticleSystem ExplosionEffect;

    [Header("Components")]
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Health health;

    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        if (renderer == null) { renderer = GetComponentInChildren<SpriteRenderer>(); }
        if (rigidbody == null) { rigidbody = GetComponentInChildren<Rigidbody2D>(); }
        if (collider == null) { collider = GetComponentInChildren<Collider2D>(); }
        if (health == null) { health = GetComponentInChildren<Health>(); }

        health.OnDeath += Die;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Player.GetHealth().IsDead() && CanMove) {
            if (CanInputMove) {
                Vector3 dir = Player.GetPosition() - this.transform.position;
                dir = dir.normalized;

                velocity += ((dir * Speed) - velocity) * Acceleration;
            }

            this.transform.position += velocity * Time.deltaTime;
        } 
    }

    public void Die() {
        renderer.enabled = false;
        collider.enabled = false;
        CanMove = false;

        // Particle
        ExplosionEffect.Play();

        Destroy(this.gameObject, 1.0f);
    }

    public void Knockback(Vector3 force) {
        velocity += force;
    }

    public void Stun(float time) {
        StartCoroutine(StunEffect(time));
    }

    private IEnumerator StunEffect(float time) {
        CanInputMove = false;
        yield return new WaitForSeconds(time);
        CanInputMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject == Player.GetGameObject()) {
            Player.GetHealth().TakeDamage(Damage);
        }
    }

}
