using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public delegate void PublicAction();

    [HideInInspector]
    public float CurrentHealth;
    public float MaxHealth = 1f;

    public Action OnDeath;
    public Action<float> OnHeal;
    public Action<float> OnDamage;

    private void Start() {
        Reset();
    }

    public void Reset() {
        CurrentHealth = MaxHealth;
    }

    public float Heal(float health) {
        if (CurrentHealth + health > MaxHealth) {
            health = MaxHealth - CurrentHealth;
        }

        CurrentHealth += health;

        print(this.name + " (Heal): " + CurrentHealth + "/" + MaxHealth);

        OnHeal?.Invoke(health);
        return health;
    }

    public float TakeDamage(float damage) {
        CurrentHealth -= damage;
        print(this.name + " (Damage): " + CurrentHealth + "/" + MaxHealth);

        OnDamage?.Invoke(damage);

        if (IsDead()) {
            OnDeath?.Invoke();
        }

        return damage;
    }

    public bool IsDead() {
        return CurrentHealth <= 0.0f;
    }
}
