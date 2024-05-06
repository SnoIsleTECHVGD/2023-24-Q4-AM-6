using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBG : MonoBehaviour
{
    private Animator animator;
    private float waitTime = 0;

    public float minTime = 3.5f;
    public float maxTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        waitTime = Random.Range(1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0)
        {
            animator.SetBool("Glitch", true);
            waitTime = Random.Range(minTime, maxTime);
        }
        else
            waitTime -= Time.deltaTime;
    }
}
