using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    public float healthObject = 100f;
    
    public void HitDamage(float damage)
    {
        healthObject -= damage;
        if(healthObject <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
