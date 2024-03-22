using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    [SerializeField]
    private Rigidbody2D body2D;

    [SerializeField]
    private KeyCode button;

    [SerializeField]
    private GameObject grapplePrefab;
    private GameObject currentGrapple;

    // Variables
    private bool canGrapple = true;
    private bool isGrappling = false;
    private float grappleDuration = 0;

    // Data
    private float timeToDestroy = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button))
            Activate();

        if (isGrappling == true)
        {
            // Update grapple logic
            grappleDuration += Time.deltaTime;


        }
    }

    // Functions

    void Activate()
    {
        if (isGrappling == true || canGrapple == false)
            return;

        isGrappling = true;
        grappleDuration = 0;

        print("grapple");

        currentGrapple = Instantiate(grapplePrefab, transform.position, Quaternion.identity, transform.parent);
        currentGrapple.transform.localScale = new Vector3(currentGrapple.transform.localScale.x, 0.5f, 0);

        // Rotate to Face Mouse

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        currentGrapple.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        Destroy(currentGrapple, timeToDestroy);
    }

    void Cancel()
    {
        if (isGrappling == false)
            return;

        isGrappling = false;
    }

    // Helpers
    /*
    Vector2 GetMouseDirection()
    {
        Vector3 dir = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        dir = Camera.main.ScreenToWorldPoint(dir);
        dir = dir - transform.position;

        return new Vector2(dir.x, dir.y);
    }
    */
}
