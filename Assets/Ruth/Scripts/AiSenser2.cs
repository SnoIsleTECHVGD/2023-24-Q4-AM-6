using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSenser2 : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    private GameObject player;

    private bool hasLineOfSight = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLineOfSight)
        {
             transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        Debug.DrawLine(transform.position, player.transform.position);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if(ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Player");
            if(hasLineOfSight)
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.white);
            }
        }
    }
}
