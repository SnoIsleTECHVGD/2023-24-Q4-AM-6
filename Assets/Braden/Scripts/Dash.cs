using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;
    private SpriteRenderer sprite;

    [SerializeField]
    private Rigidbody2D body2D;

    [SerializeField]
    private KeyCode button;

    // Variables
    public bool isDashing = false;
    public bool canDash = true;

    private float dashLength = 0;
    private float dashCooldown = 0;
    private Animator animator;

    private Vector2 direction;

    // Constants
    [SerializeField]
    private float dashTime = 0.5f;
    public float dashCooldownTime = 1.25f;
    private float baseGravity;

    [SerializeField]
    private float dashSpeed = 10;

    //SFX
    public AudioSource dashSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
        }
        else
        {
            if (controller.CheckGrounding())
                dashCooldown -= Time.deltaTime * 3;
            else
                dashCooldown -= Time.deltaTime;

            if (dashCooldown <= 0)
                sprite.color = new Color(1, 1, 1);
        }

    }

    public void Activate()
    {
        if (canDash == false || isDashing == true || dashCooldown > 0 || controller.isActive == false || Time.timeScale == 0)
            return;

        isDashing = true;
        dashLength = dashTime;

        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            direction += Vector2.up;
        if (Input.GetKey(KeyCode.A))
            direction += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector2.right;

        if (direction == Vector2.zero)
            direction = Vector2.up;

        body2D.gravityScale = 0f;

        body2D.velocity = Vector2.zero;
        body2D.AddForce(direction.normalized * dashSpeed, ForceMode2D.Impulse);
        animator.SetBool("Jumping", false);
        animator.SetBool("Dash", true);
        sprite.color = new Color(0.75f, 0.75f, 0.75f);
        dashSound.Play();
    }

    public void Cancel()
    {
        if (isDashing == false)
        {
            return;
        }

        animator.SetBool("Dash", false);

        isDashing = false;
        dashLength = 0;

        body2D.gravityScale = baseGravity;

        dashCooldown = dashCooldownTime;
    }
}
