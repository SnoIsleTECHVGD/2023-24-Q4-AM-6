using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AiSenser : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Light2D pointLight;

    [SerializeField]
    private LayerMask layermask;
    private Rigidbody2D body;

    public float speed;
    public float detectDistance;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        pointLight.pointLightOuterRadius = detectDistance;
        pointLight.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        pointLight.pointLightOuterRadius = detectDistance;

        float distance = Vector2.Distance(body.position, player.transform.position);

        if (CanSeePlayer(distance) == true)
        {
            Vector3 movePos = PlayerMoveToPosition();
            body.position = Vector2.MoveTowards(body.position, movePos, speed * Time.deltaTime);

            /*
            if (movePos.x > body.position.x)
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);*/

            pointLight.color = new Color(1, 0, 0); // red
        } else
        {
            pointLight.color = new Color(1, 1, 1); // white
        }
    }

    // Helper Function
    bool CanSeePlayer(float distance)
    {
        if (distance < detectDistance)
        {
            RaycastHit2D ray = Physics2D.Raycast(body.position, player.transform.position - transform.position, 53853, layermask);

            if (ray.collider && ray.collider.gameObject.CompareTag("Player"))
            {
                if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x)
                    return true;
                else if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x)
                    return true;
            }
        }

        return false;
    }

    Vector3 PlayerMoveToPosition() // prevent him trying to jump up to the player
    {
        return new Vector3(player.transform.position.x, body.position.y, player.transform.position.z);
    }
}
