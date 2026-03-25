using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] int damageAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount); // ? for nullcheck 
    }
}
