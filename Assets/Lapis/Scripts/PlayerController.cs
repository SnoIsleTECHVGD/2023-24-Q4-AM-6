using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D body2D;

    public float buildup;
    public float maxspeed;
    public float jumpspeed;
    public float coyotetime;

    [SerializeField]
    private Grapple grapple;
    [SerializeField]
    private Dash dash;
    [SerializeField]
    private Volume volume;
    private Animator animator;

    //Ground Detect
    [SerializeField]
    private BoxCollider2D foot;
    [SerializeField]
    private LayerMask groundMask;

    //Private Data
    private float timeSinceGrounded = 0;
    private bool isJumping = false;
    private float jumpTime = 0;

    public bool isActive = true;
    public bool canJump = true;

    //SFX
    public AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grapple.grappleState == "Grapple" || isActive == false || body2D.simulated == false)
        {
            //if (grapple.grappleState != "Grapple")
                //body2D.velocity = Vector2.zero;

            animator.SetBool("Mog", false);
            animator.SetBool("Jumping", false);
            return;
        }
        else if (dash.isDashing == true || Time.timeScale == 0)
            return;

        float buildUpDelta = (buildup * 1000) * Time.deltaTime;
        float thisMaxSpeed = maxspeed;

        if (volume.profile.name == "GlitchProfile" || volume.profile.name == "TitleProfile")
            thisMaxSpeed /= 2;
        else if (volume.profile.name == "FinalProfile")
        {
            float ratio = Mathf.Clamp(0.8f - volume.weight, 0.05f, 1);
            thisMaxSpeed *= ratio;
            animator.speed = ratio;
        }

        // Left-Right Movement
        if (Input.GetKey(KeyCode.A))
        {
            body2D.AddForce(Vector2.left * buildUpDelta);

            if (grapple.isGrappling == false)
                animator.SetInteger("Dir", 1);

            animator.SetBool("Mog", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            body2D.AddForce(Vector2.right * buildUpDelta);

            if (grapple.isGrappling == false)
                animator.SetInteger("Dir", 0);

            animator.SetBool("Mog", true);
        }
        else
        {
            animator.SetBool("Mog", false);
        }
        // Jumping

        if (CheckGrounding())
            timeSinceGrounded = 0;
        else
            timeSinceGrounded += Time.deltaTime;

        if (isJumping == true)
        {
            jumpTime += Time.deltaTime;

            if (jumpTime >= 0.25f && timeSinceGrounded == 0)
            {
                isJumping = false;
                animator.SetBool("Jumping", false);
            } else if (jumpTime >= 0.35f)
                animator.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && canJump == true && timeSinceGrounded <= coyotetime)
        {
            isJumping = true;
            jumpTime = 0;
            body2D.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            jumpSound.Play();
        }

        // Speed Clamp
        body2D.velocity = new Vector2(Mathf.Clamp(body2D.velocity.x, -thisMaxSpeed, thisMaxSpeed), Mathf.Clamp(body2D.velocity.y, -jumpspeed, jumpspeed));
    }

    // Public

    public bool CheckGrounding()
    {
        RaycastHit2D[] cast = new RaycastHit2D[1];

        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = groundMask;

        int results = foot.Cast(Vector2.down, filter, cast, 0.2f, true);
        return results > 0;
    }

    public void Enable(bool State)
    {
        if (State == isActive)
            return;

        isActive = State;
    }
    
}
