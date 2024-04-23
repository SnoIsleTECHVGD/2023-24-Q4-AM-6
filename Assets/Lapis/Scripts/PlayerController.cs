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

    //Ground Detect
    [SerializeField]
    private BoxCollider2D foot;
    [SerializeField]
    private LayerMask groundMask;

    //Private Data
    private float timeSinceGrounded = 0;
    private bool isJumping = false;
    private float jumpTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (grapple.grappleState == "Grapple")
        {
            body2D.velocity = Vector2.zero;
            return;
        }
        else if (dash.isDashing == true)
            return;

        float buildUpDelta = (buildup * 1000) * Time.deltaTime;
        float thisMaxSpeed = maxspeed;

        if (volume.profile.name == "GlitchProfile")
            thisMaxSpeed /= 2;

        // Left-Right Movement
        if (Input.GetKey(KeyCode.A))
        {
            body2D.AddForce(Vector2.left * buildUpDelta);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body2D.AddForce(Vector2.right * buildUpDelta);
        }

        // Jumping
        CheckGrounding();

        if (isJumping == true)
        {
            jumpTime += Time.deltaTime;

            if (jumpTime >= 0.25f && timeSinceGrounded == 0)
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && timeSinceGrounded <= coyotetime)
        {
            isJumping = true;
            jumpTime = 0;
            body2D.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
        }

        // Speed Clamp
        body2D.velocity = new Vector2(Mathf.Clamp(body2D.velocity.x, -thisMaxSpeed, thisMaxSpeed), Mathf.Clamp(body2D.velocity.y, -jumpspeed, jumpspeed));
    }

    public void CheckGrounding()
    {
        RaycastHit2D[] cast = new RaycastHit2D[1];

        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = groundMask;

        int results = foot.Cast(Vector2.down, filter, cast, 0.2f, true);

        if (results > 0)
            timeSinceGrounded = 0;
        else
            timeSinceGrounded += Time.deltaTime;
    }

}
