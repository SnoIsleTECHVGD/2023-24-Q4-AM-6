using UnityEngine;

public class CamFollow : MonoBehaviour
    // Braden wrote this code, i just pulled it from the Q2 project
{
    public Transform target;

    public bool setCameraOnRun;
    public float speed;
    public Vector3 offset;
    void Start()
    {
        if (setCameraOnRun == true)
        {
            transform.position = target.position + offset;
        }
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed) + offset;
        }
    }
}
