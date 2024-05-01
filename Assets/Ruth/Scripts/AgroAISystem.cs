using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroAISystem : MonoBehaviour
{
    //[SerializeField]
    public Transform player;

    //[SerializedField]
    public float agroRange;

    //[SerializedField]
    public float moveSpeed;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distToPlayer < agroRange)
        {
            //code to chase player
            ChasePlayer();
        }
        else
        {
            //stop chasing player
            StopChasingPlayer();
        }


         void ChasePlayer()
        {
            if(transform.position.x < player.position.x)
            {
                //enemy is to the left side of the player, so move left
                rb2d.velocity = new Vector2(moveSpeed, 0);
            }
            else if (transform.position.x > player.position.x)
            {
                //enemy is to the right side of the player, so move left
                rb2d.velocity = new Vector2(-moveSpeed, 0);
            }
        }

         void StopChasingPlayer()
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }
}
