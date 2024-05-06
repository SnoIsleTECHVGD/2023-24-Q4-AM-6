using System;
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
    private Dash dash;

    [SerializeField]
    private KeyCode button;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PhysicsMaterial2D playerfriction;
    [SerializeField]
    private PhysicsMaterial2D nofriction;

    [SerializeField]
    private GameObject grapplePrefab;
    private GameObject currentGrapple;
    private LineRenderer grappleLine;

    [SerializeField]
    private Transform shoulder;
    [SerializeField]
    private SpriteRenderer arm;

    // Variables
    public bool canGrapple = true;

    [HideInInspector]
    public bool isGrappling = false;

    public float grappleDuration = 0;
    private float grappleCooldown = 0;

    [HideInInspector]
    public string grappleState = "";

    // Constants
    [SerializeField]
    private float timeToDestroy = 1f;
    public float grappleCooldownTime = 0.15f;
    public float jumpPower = 5;

    private float baseGravity;
    private Vector3 hitPosition;

    public float grappleSpeed = 15;
    public float grapplePullSpeed = 1f;

    //SFX
    public AudioSource grappleHit;
    public AudioSource grappleFire;

    private void Start()
    {
        baseGravity = body2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button))
        {
            Activate();
            grappleFire.Play();
        }
        

        if (isGrappling == true)
        {
            if (dash.isDashing == true || body2D.simulated == false || controller.isActive == false)
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
        }

        if (grappleCooldown > 0)
            grappleCooldown -= Time.deltaTime;
    }

    // OnUpdate
    void CheckForGrappleHit()
    {
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

                GetComponent<PolygonCollider2D>().sharedMaterial = nofriction;
                grappleCooldown = grappleCooldownTime;
                grappleHit.Play();
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
        grappleLine.SetPositions(new Vector3[] { shoulder.position, currentGrapple.transform.position });
    }
    void GrappleHitUpdate()
    {
        // Grapple has hit

        Vector3 diff = (currentGrapple.transform.position - transform.position).normalized / 2;

        Vector3 goal = hitPosition - diff;
        Vector2 goalv2 = new Vector2(goal.x, goal.y);

        body2D.gravityScale = 0;

        // Pull

        Vector2 target = (goalv2 - body2D.position);
        target.Normalize();

        body2D.velocity = target * (grapplePullSpeed / 1.5f);
        grappleLine.SetPositions(new Vector3[] { shoulder.position, hitPosition });

        // Update Arm

        Vector3 diff2 = hitPosition - shoulder.position;
        diff2.Normalize();

        float rot_z = Mathf.Atan2(diff2.y, diff2.x) * Mathf.Rad2Deg;
        shoulder.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

        // Cancel

        if (Input.GetKey(KeyCode.Space))
        {    
            Cancel();
            body2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    // Functions

    void Activate()
    {
        if (canGrapple == false || grappleCooldown > 0 || dash.isDashing == true || body2D.simulated == false || Time.timeScale == 0 || controller.isActive == false)
            return;

        if (isGrappling == true)
        {
            if (grappleState != "Grapple")
                return;

            Cancel();
        }  

        isGrappling = true;
        grappleDuration = 0;

        // Rotate to Face Mouse

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        // Animator

        if (rot_z >= -90 && rot_z <= 90) // right
        {
            animator.SetInteger("Dir", 0);
            shoulder.transform.localPosition = new Vector3(0.501f, 0.34f, 0);
        } 
        else // left
        {
            animator.SetInteger("Dir", 1);
            shoulder.transform.localPosition = new Vector3(-0.501f, 0.34f, 0);
        }

        animator.SetBool("Grapple", true);
        arm.enabled = true;

        // Make the Grapple

        currentGrapple = Instantiate(grapplePrefab, shoulder.position, Quaternion.identity, transform);

        grappleLine = currentGrapple.GetComponent<LineRenderer>();
        grappleLine.SetPositions(new Vector3[] { shoulder.position, shoulder.position });

        currentGrapple.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        shoulder.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }

    public void Cancel()
    {
        if (isGrappling == false)
            return;

        if (grappleState == "Grapple")
            GetComponent<PolygonCollider2D>().sharedMaterial = playerfriction;

        isGrappling = false;
        grappleState = "";

        animator.SetBool("Grapple", false);
        arm.enabled = false;

        body2D.gravityScale = baseGravity;

        Destroy(currentGrapple);
        currentGrapple = null;
    }
}
