using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    Explosion TakeDamage(int damage);

    void OnDeath();
}

public abstract class DamageableEnity : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public Explosion hitEffect;
    public Explosion deathEffect;

    int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual Explosion TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0) { OnDeath(); }
        return hitEffect;
    }

    public abstract void OnDeath();
}
