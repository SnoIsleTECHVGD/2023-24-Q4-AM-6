using System;
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

    [SerializeField]
    private GameObject grapplePrefab;
    private GameObject currentGrapple;
    private LineRenderer grappleLine;

    // Variables
    public bool canGrapple = true;
    public bool isGrappling = false;

    public float grappleDuration = 0;
    public string grappleState = "";

    // Data
    [SerializeField]
    private float timeToDestroy = 1f;
    private Vector3 hitPosition;

    public float grappleSpeed = 15;
    public float grapplePullSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button))
            Activate();

        if (isGrappling == true)
        {
            grappleDuration += Time.deltaTime;

            // Update grapple logic

            if (grappleState == "")
                GrappleNoHitUpdate();
            else if (grappleState == "Grapple")
                GrappleHitUpdate();

            // Check for hit

            if (grappleState == "")
                CheckForGrappleHit();
        }
    }

    // OnUpdate
    void CheckForGrappleHit()
    {
        RaycastHit2D hit = Physics2D.Linecast(grappleLine.GetPosition(0), grappleLine.GetPosition(1), LayerMask.NameToLayer("GrappleHook"));

        if (hit)
        {
            if (hit.transform.gameObject.CompareTag("Grapple"))
            {
                grappleState = "Grapple";
                grappleDuration = 0;  
                hitPosition = currentGrapple.transform.position;
            }
            else
                grappleState = "Wall";
        }

        if ((grappleState == "" && grappleDuration > timeToDestroy) || grappleState == "Wall")
            Cancel();
    }

    void GrappleNoHitUpdate()
    {
        // Grapple has not hit

        float amount = grappleSpeed * Time.deltaTime;

        currentGrapple.transform.Translate(currentGrapple.transform.up * (amount / 2), Space.World);
        grappleLine.SetPositions(new Vector3[] { body2D.position, currentGrapple.transform.position });
    }
    void GrappleHitUpdate()
    {
        // Grapple has hit

        Vector3 diff = (currentGrapple.transform.position - transform.position).normalized / 2;
        Vector3 goal = hitPosition - diff;

        body2D.gravityScale = 0;

        if ((transform.position - goal).magnitude >= 0.4f)
        {
            body2D.position = Vector3.Lerp(body2D.position, goal, grappleDuration * (grapplePullSpeed / 100));
            grappleLine.SetPositions(new Vector3[] { body2D.position, hitPosition });
        } 
        else
        {
            body2D.position = goal;
            grappleLine.enabled = false;

            Cancel();
        }
    }

    // Functions

    void Activate()
    {
        if (isGrappling == true || canGrapple == false)
            return;

        isGrappling = true;
        grappleDuration = 0;

        currentGrapple = Instantiate(grapplePrefab, transform.position, Quaternion.identity, transform);

        grappleLine = currentGrapple.GetComponent<LineRenderer>();
        grappleLine.SetPositions(new Vector3[] { transform.position, transform.position });

        // Rotate to Face Mouse

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        currentGrapple.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void Cancel()
    {
        if (isGrappling == false)
            return;

        isGrappling = false;
        grappleState = "";

        body2D.gravityScale = 1;

        Destroy(currentGrapple);
        currentGrapple = null;
    }
}
