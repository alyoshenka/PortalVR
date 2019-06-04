using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : DamageableEnity
{
    public Image damageFlash;
    [Range(0, 1)]
    public float maxAlpha;
    public float damageLerp;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDeath()
    {
        Debug.Log("you died");
    }

    public override Explosion TakeDamage(int damage)
    {
        StartCoroutine("DamageFlash");
        return base.TakeDamage(damage);
    }

    IEnumerator DamageFlash()
    {
        float t = damageFlash.color.a / maxAlpha;
        for(float i = 0; i < damageLerp / 3; i += Time.deltaTime)
        {
            // damageFlash.color = 
            yield return null;
        }
    }
}
