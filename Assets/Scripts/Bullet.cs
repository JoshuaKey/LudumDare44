using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public float Damage;
    public Vector2 Force;

    [Header("Components")]
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private new Rigidbody2D rigidbody;

    private void Start() {
        if (renderer == null) { renderer = GetComponentInChildren<SpriteRenderer>(); }
        if (rigidbody == null) { rigidbody = GetComponentInChildren<Rigidbody2D>(); }
        if (collider == null) { collider = GetComponentInChildren<Collider2D>(); }
    }

    public void Fire() {
        rigidbody.AddForce(Force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //print("Hit " + collision.name);

        Health health = collision.GetComponentInChildren<Health>();
        if(health != null) {
            health.TakeDamage(Damage);
            Destroy(this.gameObject);
        }
        
    }
}
