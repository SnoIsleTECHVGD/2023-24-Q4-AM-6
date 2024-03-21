using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    [SerializeField]
    private Rigidbody2D body2D;

    [SerializeField]
    private KeyCode button;

    // Variables
    private bool canGrapple = true;
    private bool isGrappling = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button))
            Activate();
    }

    // Functions

    void Activate()
    {
        if (isGrappling == true || canGrapple == false)
            return;

        //isGrappling = true;

        Vector2 direction = GetMouseDirection();
        //Instantiate()

        print("grapple");
    }

    void Cancel()
    {
        if (isGrappling == false)
            return;

        isGrappling = false;
    }

    // Helpers

    Vector2 GetMouseDirection()
    {
        Vector3 dir = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        dir = Camera.main.ScreenToWorldPoint(dir);
        dir = dir - transform.position;

        return new Vector2(dir.x, dir.y);
    }
}
