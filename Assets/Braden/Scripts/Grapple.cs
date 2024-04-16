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
    private Dash dash;

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
    private float grappleCooldown = 0;

    public string grappleState = "";

    // Constants
    [SerializeField]
    private float timeToDestroy = 1f;
    public float grappleCooldownTime = 0.75f;
    public float jumpPower = 5;

    private Vector3 hitPosition;

    public float grappleSpeed = 15;
    public float grapplePullSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button) && Time.timeScale != 0)
            Activate();

        if (isGrappling == true)
        {
            if (dash.isDashing == true)
            {
                Cancel();
                return;
            }

            grappleDuration += Time.deltaTime;

            // Update grapple logic

            if (grappleState == "")
                GrappleNoHitUpdate();
            else if (grappleState == "Grapple")
                GrappleHitUpdate();

            // Check for hit

            if (isGrappling == true && grappleState == "")
                CheckForGrappleHit();
        } else
            grappleCooldown -= Time.deltaTime;
    }

    // OnUpdate
    void CheckForGrappleHit()
    {
        //RaycastHit2D hit = Physics2D.Linecast(grappleLine.GetPosition(0), grappleLine.GetPosition(1), LayerMask.NameToLayer("GrappleHook"));
        //RaycastHit2D hit = Physics2D.Raycast(grappleLine.GetPosition(1), currentGrapple.transform.up, (grappleSpeed * Time.deltaTime) / 4, LayerMask.NameToLayer("GrappleHook"));
        RaycastHit2D hit = Physics2D.BoxCast(grappleLine.GetPosition(1),
            new Vector2(grappleLine.startWidth, grappleLine.startWidth), currentGrapple.transform.localEulerAngles.z,
            currentGrapple.transform.up, Time.deltaTime, LayerMask.NameToLayer("GrappleHook"));

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
            body2D.position = goal;

        if (Input.GetKey(KeyCode.Space))
        {
            body2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            Cancel();
        }
    }

    // Functions

    void Activate()
    {
        if (isGrappling == true || canGrapple == false || grappleCooldown > 0 || dash.isDashing == true)
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

        grappleCooldown = grappleCooldownTime;
        isGrappling = false;
        grappleState = "";

        body2D.gravityScale = 1;

        Destroy(currentGrapple);
        currentGrapple = null;
    }
}
