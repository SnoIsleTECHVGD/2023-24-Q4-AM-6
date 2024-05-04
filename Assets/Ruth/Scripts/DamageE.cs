using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageE : MonoBehaviour
{
    public int damage; 
    public PlayerController player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<HealthE>().TakeDamage(1);
        }
    }
}
