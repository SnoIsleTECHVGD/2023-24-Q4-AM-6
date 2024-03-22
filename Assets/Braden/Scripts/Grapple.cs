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
    private Rigidbody2D grappleBody;
    private HookCollide hookCollide;

    // Variables
    private bool canGrapple = true;
    private bool isGrappling = false;
    private float grappleDuration = 0;

    // Data
    [SerializeField]
    private float timeToDestroy = 1f;

    [SerializeField]
    private float grappleSpeed = 15;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button))
            Activate();

        if (isGrappling == true)
        {
            // Update grapple logic

            if (hookCollide.collideState != "Grapple")
            {
                // Grapple has not hit
                grappleDuration += Time.deltaTime;

                if (grappleDuration > timeToDestroy || hookCollide.collideState == "Wall")
                {
                    Cancel();
                    return;
                }

                float amount = grappleSpeed * Time.deltaTime;

                // ALTERNATIVE: grappleBody.velocity = grappleBody.transform.up * (amount * 30);
                grappleBody.transform.Translate(grappleBody.transform.up * (amount / 2), Space.World);

                grappleBody.transform.localScale = new Vector3(grappleBody.transform.localScale.x, grappleBody.transform.localScale.y + amount, 0);
                
            } else
            {
                // Grapple has hit
                Cancel();
            }
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

        grappleBody = currentGrapple.GetComponent<Rigidbody2D>();
        grappleBody.isKinematic = true;

        hookCollide = currentGrapple.GetComponent<HookCollide>();

        // Rotate to Face Mouse

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        grappleBody.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void Cancel()
    {
        if (isGrappling == false)
            return;

        isGrappling = false;

        Destroy(currentGrapple);
        currentGrapple = null;
    }
}
