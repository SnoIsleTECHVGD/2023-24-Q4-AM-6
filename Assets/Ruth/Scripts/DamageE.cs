using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageE : MonoBehaviour
{
    public int damage; 
    public HealthE health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            health.TakeDamage(damage);
        }
    }
}
