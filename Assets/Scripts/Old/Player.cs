using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : DamageableEnity
{ 
    public Image damageFlash;
    public Color damageColor;
    [Tooltip("The opacity of the flash at start")]
    [Range(0, 1)]
    public float maxAlpha;
    [Tooltip("The duration of the flash")]
    public float damageLerp;    
    [Tooltip("The radius of the head collider (0.1f)")]
    public float headRadius;
    public Image healthBar;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        damageFlash.color = damageColor;
        damageFlash.enabled = false;
        GetComponent<SphereCollider>().radius = headRadius;
    }

    public override void OnDeath()
    {
        Debug.Log("you died");
    }

    public override Explosion TakeDamage(int damage)
    {
        StopCoroutine("DamageFlash");
        StartCoroutine("DamageFlash");

        base.TakeDamage(damage);
        healthBar.fillAmount = 1.0f * currentHealth / maxHealth;
        return hitEffect;
    }

    IEnumerator DamageFlash()
    {
        damageFlash.enabled = true;
        Color end = damageColor;
        damageColor.a = maxAlpha;
        end.a = 0;
        for(float t = 0; t < damageLerp; t += Time.deltaTime)
        {
            damageFlash.color = Color.Lerp(damageColor, end, t);
            yield return null;
        }
        damageFlash.enabled = false;
    }

    public void RefillHealth(int points)
    {
        currentHealth += points;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = 1.0f * currentHealth / maxHealth;
    }

}
