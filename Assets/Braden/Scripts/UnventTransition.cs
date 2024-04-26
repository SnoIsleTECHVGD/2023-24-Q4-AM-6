using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnventTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private CamFollow camFollow;

    private float orthosize;
    private bool isZooming = false;

    void Start()
    {
        orthosize = mainCamera.orthographicSize;
        mainCamera.orthographicSize = 0;
        camFollow.target = transform;

        player.GetComponent<Rigidbody2D>().simulated = false;

        isZooming = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isZooming)
        {
            mainCamera.orthographicSize += Time.deltaTime * 7.5f;

            if (mainCamera.orthographicSize >= 3)
                player.GetComponent<Rigidbody2D>().simulated = true;

            if (mainCamera.orthographicSize >= orthosize)
                Complete();
        }
    }

    void Complete()
    {
        isZooming = false;
        mainCamera.orthographicSize = orthosize;
        camFollow.target = player.transform;
    }
}
