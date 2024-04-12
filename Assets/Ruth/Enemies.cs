using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Transform movement;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.5f;
    int direction = 1;


    private void Update()
    {
        Vector2 target = currentMovementTarget();

        movement.position = Vector2.Lerp(movement.position, target, speed * Time.deltaTime);

        float distance=(target - (Vector2)movement.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        // just for debug visual
        if(movement != null && startPoint!=null && endPoint != null)
        {
            Gizmos.DrawLine(movement.transform.position, startPoint.position);
            Gizmos.DrawLine(movement.transform.position, endPoint.position);
        }
    }
}
