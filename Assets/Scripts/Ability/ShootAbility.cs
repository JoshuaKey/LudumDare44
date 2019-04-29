using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbility : Ability {

    public bool UsePlayerDirection = false;

    public override void Init() {
        base.Init();
        UsePlayerDirection = Random.value < .5f;
    }

    protected override void Activate() {
        base.Activate();

        Vector3 dir = Vector3.zero;
        if (UsePlayerDirection) {
            dir = Player.GetVelocity() == Vector3.zero ? Vector3.right : Player.GetVelocity().normalized;
        } else {
            Vector3 mousePos = Input.mousePosition;
            Vector3 aimPos = Player.GetCamera().ScreenToWorldPoint(mousePos);
            aimPos.z = 0.0f;
            dir = (aimPos - Player.GetPosition()).normalized;
        }

        Vector3 pos = Player.GetPosition();
        Quaternion rot = Quaternion.FromToRotation(Vector3.right, dir);
        Bullet bullet = GameObject.Instantiate(BulletPrefab, pos, rot);

        bullet.Damage = Damage;
        bullet.Force = dir * BulletForce;
        bullet.Fire();

        Player.Knockback(-dir);
    }
}
