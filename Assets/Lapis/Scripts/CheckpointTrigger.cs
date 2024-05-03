using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{ 
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
    }
}
