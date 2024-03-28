using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    [SerializeField]
    private Rigidbody2D body2D;

    [SerializeField]
    private KeyCode button;

    // Variables
    public bool isDashing = false;
    public bool canDash = true;

    private float dashLength = 0;
    private float dashCooldown = 0;

    private Vector2 direction;

    // Constants
    [SerializeField]
    private float dashTime = 0.5f;
    public float dashCooldownTime = 0.75f;

    [SerializeField]
    private float dashSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button))
            Activate();

        if (isDashing == true)
        {
            if (dashLength <= 0)
                Cancel();

            dashLength -= Time.deltaTime;
        } else
            dashCooldown -= Time.deltaTime;
    }

    public void Activate()
    {
        if (canDash == false || isDashing == true || dashCooldown > 0)
            return;

        isDashing = true;
        dashLength = dashTime;

        if (Input.GetKey(KeyCode.A))
            direction = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            direction = Vector2.right;
        else
            direction = Vector2.up;

        body2D.gravityScale = 0f;

        body2D.velocity = Vector2.zero;
        body2D.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
    }

    public void Cancel()
    {
        if (isDashing == false) 
            return;

        isDashing = false;
        dashLength = 0;

        body2D.gravityScale = 1;
        dashCooldown = dashCooldownTime;
    }
}
