using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCollide : MonoBehaviour
{
    [HideInInspector]
    public string collideState = "";
    public Vector3 hitPosition;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collideState != "")
            return;

        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Grapple"))
        {
            collideState = "Grapple";
            hitPosition = transform.position + (transform.localScale / 2);
        } else
        {
            if (obj.layer != LayerMask.NameToLayer("Player"))
            {
                collideState = "Wall";
            }
        }

        print(collideState);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collideState != "")
            return;

        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Grapple"))
        {
            collideState = "Grapple";
            hitPosition = collision.GetContact(0).point;
        }
        else
        {
            if (obj.layer != LayerMask.NameToLayer("Player"))
            {
                collideState = "Wall";
            }
        }

        print(collideState);
    }
}
