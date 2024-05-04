using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthE : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;

    [HideInInspector]
    public bool isAlive = true;

    // Start is called before the first frame update

    void Start()
    {
        health = maxHealth;
    }

    // Public

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        if (health <= 0)
            Kill();
    }

    public void Kill()
    {
        if (!isAlive)
            return;

        isAlive = false;

        if (gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<PlayerController>().Enable(false);
            GetComponent<Animator>().SetBool("Dead", true);

            GetComponent<Glitch>().Activate();
        } else
        {
            Destroy(gameObject);
        }
    }
}
