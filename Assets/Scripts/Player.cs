using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : DamageableEnity
{ 
    public Image damageFlash;
    public Explosion damageExplosion;
    public Color damageColor;
    [Tooltip("The opacity of the flash at start")]
    [Range(0, 1)]
    public float maxAlpha;
    [Tooltip("The duration of the flash")]
    public float damageLerp;    
    [Tooltip("The radius of the head collider (0.1f)")]
    public float headRadius;

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
        return damageExplosion;
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

}
