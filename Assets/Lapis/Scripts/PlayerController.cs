using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D body2D;
    public float movespeed;
    public float jumpspeed;
    public float maxspeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body2D.AddForce(Vector2.left * movespeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body2D.AddForce(Vector2.right * movespeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body2D.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
        }
        body2D.velocity = new Vector2(Mathf.Clamp(body2D.velocity.x, -maxspeed, maxspeed), Mathf.Clamp(body2D.velocity.y, -jumpspeed, jumpspeed));
    }
    //yoo time to code minesweeper (no)
}
