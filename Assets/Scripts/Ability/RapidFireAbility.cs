using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFireAbility : Ability {

    public bool UsePlayerDirection = false;
    public int BulletCount;
    public float BulletDelay;

    public override void Init() {
        base.Init();
        UsePlayerDirection = Random.value < .5f;

        BulletCount = Random.Range(2, 6);
        BulletDelay = Random.Range(.05f, 1);
    }

    protected override void Activate() {
        base.Activate();

        Player.StaticStartCoroutine(RapidFire());
    }

    private IEnumerator RapidFire() {
        WaitForSeconds wait = new WaitForSeconds(BulletDelay);

        Vector3 dir = Vector3.zero;
        if (UsePlayerDirection) {
            dir = Player.GetVelocity() == Vector3.zero ? Vector3.right : Player.GetVelocity().normalized;
        } else {
            Vector3 mousePos = Input.mousePosition;
            Vector3 aimPos = Player.GetCamera().ScreenToWorldPoint(mousePos);
            aimPos.z = 0.0f;
            dir = (aimPos - Player.GetPosition()).normalized;
        }

        for (int i = 0; i < BulletCount; i++) {
            SpawnBullet(dir);
            yield return wait;
        }
    }
 
    private void SpawnBullet(Vector3 dir) {
        Vector3 pos = Player.GetPosition();
        Quaternion rot = Quaternion.FromToRotation(Vector3.right, dir);
        Bullet bullet = GameObject.Instantiate(BulletPrefab, pos, rot);

        bullet.Damage = Damage;
        bullet.Force = dir * BulletForce;
        bullet.Fire();

        Player.Knockback(-dir);
    }
}
