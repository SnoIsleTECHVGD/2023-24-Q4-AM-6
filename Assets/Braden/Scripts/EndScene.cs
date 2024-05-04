using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Volume lighting;

    [SerializeField]
    private VolumeProfile mainProfile;
    [SerializeField]
    private VolumeProfile glitchProfile;

    public float minDistance;
    public float triggerDistance;

    [HideInInspector]
    public bool sceneActive = false;

    // Update is called once per frame
    void Update()
    {
        if (sceneActive)
            return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= triggerDistance)
            Activate();
        else if (distance <= minDistance)
        {
            if (lighting.profile != glitchProfile)
            {
                player.GetComponent<PlayerController>().canJump = false;

                player.GetComponent<Grapple>().Cancel();
                player.GetComponent<Grapple>().canGrapple = false;

                player.GetComponent<Dash>().Cancel();
                player.GetComponent<Dash>().canDash = false;
            }

            lighting.profile = glitchProfile;

            float ratio = distance / minDistance;
            lighting.weight = 1 - ratio;
        }
        else if (lighting.profile == glitchProfile)
        {
            lighting.profile = mainProfile;

            player.GetComponent<Grapple>().canGrapple = true;
            player.GetComponent<Dash>().canDash = true;
            player.GetComponent<PlayerController>().canJump = true;
        }
    }

    // Activate
    void Activate()
    {
        if (sceneActive)
            return;

        sceneActive = true;

        player.GetComponent<Animator>().speed = 0;
        player.GetComponent<PlayerController>().Enable(false);
    }
}
