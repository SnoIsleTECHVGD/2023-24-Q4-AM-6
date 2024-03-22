using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D body2D;
    public float buildup;
    public float maxspeed;
    public float jumpspeed;

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
        float buildUpDelta = (buildup * 1000) * Time.deltaTime;
        //movement
        if (Input.GetKey(KeyCode.A))
        {
            body2D.AddForce(Vector2.left * buildUpDelta);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body2D.AddForce(Vector2.right * buildUpDelta);
        }
        //jump
        CheckGrounding();
        if (isJumping == true)
        {
            jumpTime += Time.deltaTime;

            if (jumpTime >= 0.25f && timeSinceGrounded == 0)
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            jumpTime = 0;
            body2D.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
        }
        //speed clamp
        body2D.velocity = new Vector2(Mathf.Clamp(body2D.velocity.x, -maxspeed, maxspeed), Mathf.Clamp(body2D.velocity.y, -jumpspeed, jumpspeed));
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
