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
    public float dashCooldownTime = 1.25f;
    private float baseGravity;

    [SerializeField]
    private float dashSpeed = 10;

    private void Start()
    {
        baseGravity = body2D.gravityScale;
    }

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
        if (canDash == false || isDashing == true || dashCooldown > 0 || controller.enabled == false || Time.timeScale == 0)
            return;

        isDashing = true;
        dashLength = dashTime;

        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            direction += (Vector2.up * dashSpeed);
        if (Input.GetKey(KeyCode.S))
            direction += (Vector2.down * dashSpeed);
        if (Input.GetKey(KeyCode.A))
            direction += (Vector2.left * dashSpeed);
        if (Input.GetKey(KeyCode.D))
            direction += (Vector2.right * dashSpeed);

        if (direction == Vector2.zero)
            direction = (Vector2.up * dashSpeed);

        body2D.gravityScale = 0f;

        body2D.velocity = Vector2.zero;
        body2D.AddForce(direction, ForceMode2D.Impulse);
    }

    public void Cancel()
    {
        if (isDashing == false) 
            return;

        isDashing = false;
        dashLength = 0;

        body2D.gravityScale = baseGravity;
        dashCooldown = dashCooldownTime;
    }
}
