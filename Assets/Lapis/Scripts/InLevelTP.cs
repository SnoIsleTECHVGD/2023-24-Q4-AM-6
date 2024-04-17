using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelTP : MonoBehaviour
{
    public GameObject player;
    public GameObject TPpoint;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = TPpoint.transform.position;
        //just that easy..
    }
}
