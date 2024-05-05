using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageE : MonoBehaviour
{
    public int damage; 
    public PlayerController player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<HealthE>().TakeDamage(1);
        }
    }
}
