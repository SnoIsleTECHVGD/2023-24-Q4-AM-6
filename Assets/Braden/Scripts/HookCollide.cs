using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCollide : MonoBehaviour
{
    [HideInInspector]
    public string collideState = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collideState != "")
            return;

        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Grapple"))
        {
            collideState = "Grapple";
        } else
        {
            if (obj.layer != LayerMask.NameToLayer("Player"))
            {
                collideState = "Wall";
            }
        }

        print(collideState);
    }
}
