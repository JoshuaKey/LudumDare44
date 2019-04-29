using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability {
    
    [Header("Activation")]
    public string Button;
    public float CoolDown;

    [Header("Ability")]
    public Bullet BulletPrefab;
    public float Damage;
    public float BulletForce;

    private float nextActivateTime = 0.0f;

    public virtual void Init() {
        CoolDown = Random.Range(.2f, 5.0f);
        BulletForce = 10f;
    }

    public virtual void Check() {
        if (Input.GetButton(Button) && CanUseAbility()) {
            Activate();
        }
    }

    protected virtual void Activate() {
        nextActivateTime = Time.time + CoolDown;
    }

    public virtual bool CanUseAbility() {
        return Time.time >= nextActivateTime;
    }

    public override string ToString() {
        return this.GetType().Name + " \"" + Button + "\" - " + this.CoolDown;
    }
}
