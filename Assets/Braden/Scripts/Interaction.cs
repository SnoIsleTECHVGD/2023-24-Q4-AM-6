using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public KeyCode button = KeyCode.E;
    public bool isOneTimeEvent = true;
    public UnityEvent interactEvent;

    private bool isTouching = false;

    [HideInInspector]
    public bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            return;

        if (isTouching && Input.GetKeyDown(button))
        {
            if (isOneTimeEvent)
                isActive = true;

            interactEvent.Invoke();
        }
    }

    // Trigger

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            isTouching = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            isTouching = false;
    }
}
