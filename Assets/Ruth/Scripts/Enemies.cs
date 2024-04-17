using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Transform movement;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 0.5f;
    int direction = 1;
    float timeTravel = 0;


    private void Update()
    {
        Vector2 target = currentMovementTarget();

        if (direction < 0)
        {
            movement.position = Vector2.Lerp(startPoint.position, endPoint.position, timeTravel);
        }
        else
        {
            movement.position = Vector2.Lerp(endPoint.position, startPoint.position, timeTravel);
        }
       

        float distance=(target - (Vector2)movement.position).magnitude;

        timeTravel += Time.deltaTime * speed;

        if (distance <= 0.1f)
        {
            timeTravel = 0;
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
