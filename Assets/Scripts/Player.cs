using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("States")]
    public bool CanMove;
    public bool CanInputMove;
    public bool CanShoot;
    public bool CanUseAbility = true;

    [Header("Speed")]
    public float Speed = 5f;
    public float Acceleration = 0.1f;

    [Header("Bullets")]
    public Bullet BulletPrefab;
    public float BulletForce;
    public float AttackSpeed;
    public float Damage = 1;

    [Header("Ability")]
    // Effect...
    public float AbilityBulletForce = 5;
    public float AbilityAttackSpeed = 5;
    public float AbilityDamage = 1;

    public List<Ability> Abilities = new List<Ability>();

    [Header("Respawn")]
    public float RespawnTime = 2.0f;
    public float RespawnForce = 10.0f;
    public float RespawnDistance = 20.0f;
    public float RespawnStunTime = 1.2f;

    [Header("Effects")]
    public ParticleSystem ExplosionEffect;
    public ParticleSystem WaveEffect;

    [Header("Components")]
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private new Camera camera;
    [SerializeField] private Health health;

    private static Player Instance;

    private Vector3 velocity = Vector3.zero;
    private float nextAttack = 0.0f;
    private float nextAbilityAttack = 0.0f;
    private int EnemyMask;

    private void Awake() {
        if (Instance != null) { Destroy(this.gameObject); return; }
        Instance = this;
    }
    private void Start () {
		if(renderer == null) { renderer = GetComponentInChildren<SpriteRenderer>(); }
		if(rigidbody == null) { rigidbody = GetComponentInChildren<Rigidbody2D>(); }
		if(collider == null) { collider = GetComponentInChildren<Collider2D>(); }
		if(camera == null) { camera = Camera.main; }
        if (health == null) { health = GetComponentInChildren<Health>(); }

        EnemyMask = 1 << LayerMask.NameToLayer("Enemy");

        health.OnDeath += Die;
    }

    // Update is called once per frame
    private void Update () {
        if (CanMove) {
            Vector3 mvmt = Vector3.zero;
            if (CanInputMove) {
                mvmt.x += Input.GetAxis("Horizontal");
                mvmt.y += Input.GetAxis("Vertical");
                mvmt = mvmt.normalized;
            }

            velocity += ((mvmt * Speed) - velocity) * Acceleration;

            this.transform.position += velocity * Time.deltaTime;
        }


        if (CanUseAbility) {
            foreach (Ability a in Abilities) {
                a.Check();
            }
        }

        //// ------------------------------------------
        //if (CanUseAbility) {
        //    if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAbilityAttack) {
        //        nextAbilityAttack = Time.time + AbilityAttackSpeed;
        //        float bulletCount = 10;
        //        for (int i = 0; i < bulletCount; i++) {                
        //            float angle = (i / bulletCount) * 360;

        //            Vector3 pos = this.transform.position;
        //            Quaternion rot = Quaternion.Euler(0, 0, angle);
        //            Vector3 dir = rot * Vector3.right;
        //            Bullet bullet = Instantiate(BulletPrefab, pos, rot);

        //            bullet.Damage = Damage;
        //            bullet.Force = dir * BulletForce;
        //            bullet.Fire();
        //        }                
        //    }
        //}
    }

    public void Die() {
        print("Player Died");

        // Status
        renderer.enabled = false;
        CanShoot = false;
        CanMove = false;
        CanUseAbility = false;

        // Particle
        ExplosionEffect.Play();

        // Respawn
        StaticStartCoroutine(Respawn());
    }

    private IEnumerator Respawn() {
        // Wait
        yield return new WaitForSeconds(RespawnTime);

        // Particle
        WaveEffect.Play();

        // Knockback
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, RespawnDistance, EnemyMask);
        foreach (Collider2D c in colliders) {
            //print("Collided with " + c.name);
            Enemy e = c.GetComponentInChildren<Enemy>();
            if (e != null) {
                Vector3 dir = e.transform.position - this.transform.position;
                float magnitude = 0.0f;
                //print(dir);
                if (dir == Vector3.zero) { // Right on Top of Player
                    magnitude = RespawnDistance;
                    dir = Vector3.right;
                } else {
                    magnitude = dir.magnitude;
                    dir = dir / magnitude;
                }

                Vector3 knockbackForce = dir * RespawnForce;
                //print(dir + " " + RespawnForce);
                //print("Knockback: " + knockbackForce);

                e.Knockback(knockbackForce);
                e.Stun(RespawnStunTime);
            } else {
                print("Detected in Enemy Overlap Circle: " + c.name);
            }
        }

        // Restore
        health.Reset();
        renderer.enabled = true;
        CanShoot = true;
        CanMove = true;
        CanUseAbility = true;
    }

    #region static
    public static GameObject GetGameObject() {
        return Instance.gameObject;
    }
    public static Health GetHealth() {
        return Instance.health;
    }
    public static Camera GetCamera() {
        return Instance.camera;
    }
    public static Vector3 GetPosition() {
        return Instance.transform.position;
    }
    public static Vector3 GetVelocity() {
        return Instance.velocity;
    }
    public static List<Ability> GetAbilities() {
        return Instance.Abilities;
    }
    public static void Knockback(Vector3 force) {
        Instance.velocity += force;
    }
    public static void StaticStartCoroutine(IEnumerator routine) {
        Instance.StartCoroutine(routine);
    }
    #endregion
}
