using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSenser : MonoBehaviour

    //Ask about the dumb friend and figure out how to get the Ai senser to work better with the dumb friend and not be a brat
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;



        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        /*
        bool CanSeePlayer(float distance)
        {
            bool val = false;
            float castDist = distance;

            Vector2 endPos
            RaycastHit2D hit = Physics2D.Linecast(transform.position, )
        }
        //    GetComponentInParent<Enemies>().enabled = false;
        //}
        //else
        //{
        //    GetComponentInParent<Enemies>().enabled = true;
        //}*/
    }
}
