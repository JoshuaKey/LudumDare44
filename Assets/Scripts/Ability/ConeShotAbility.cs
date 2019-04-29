using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeShotAbility : Ability {

    public bool UsePlayerDirection = false;

    public int BulletCount = 10;
    public float ConeDegrees = 360;
    public float StartingAngle = 0;

    public override void Init() {
        base.Init();
        UsePlayerDirection = Random.value < .5f;

        BulletCount = Random.Range(4, 10);
        ConeDegrees = Random.Range(1, 5) * 90;
        if (!UsePlayerDirection) {
            StartingAngle = Random.Range(1, 5) * 90;
        }
    }

    protected override void Activate() {
        base.Activate();

        if (UsePlayerDirection) {
            Vector3 playerDir = Player.GetVelocity() == Vector3.zero ? Vector3.right : Player.GetVelocity().normalized;
            StartingAngle = Vector3.Angle(Vector3.right, playerDir);
        }

        for (float i = 0; i < BulletCount; i++) {
            float angle = (i / BulletCount) * ConeDegrees + StartingAngle;

            Vector3 pos = Player.GetPosition();
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            Vector3 dir = rot * Vector3.right;
            Bullet bullet = GameObject.Instantiate(BulletPrefab, pos, rot);

            bullet.Damage = Damage;
            bullet.Force = dir * BulletForce;
            bullet.Fire();
        }
    }

    public override string ToString() {
        return base.ToString() + "\n" + StartingAngle + " (" + ConeDegrees + ") - " + BulletCount;
    }
}
