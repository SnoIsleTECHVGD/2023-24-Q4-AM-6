using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LockCam : MonoBehaviour
{
    //objects
    public GameObject cam;
    public Transform lockpoint;
    public Transform player;
    //values
    public float camZoom;
    public int camSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.GetComponent<CamFollow>().speed = camSpeed;
        cam.GetComponent<CamFollow>().target = lockpoint;
        cam.GetComponent<Camera>().orthographicSize = camZoom; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cam.GetComponent<CamFollow>().target == lockpoint)
        {
            cam.GetComponent<CamFollow>().target = player;
            cam.GetComponent<Camera>().orthographicSize = 6;
            cam.GetComponent<CamFollow>().speed = 5;
        }
    }

}
